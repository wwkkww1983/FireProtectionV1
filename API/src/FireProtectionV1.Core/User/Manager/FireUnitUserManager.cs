﻿using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using FireProtectionV1.Common.DBContext;
using FireProtectionV1.Common.Enum;
using FireProtectionV1.Common.Helper;
using FireProtectionV1.Enterprise.Model;
using FireProtectionV1.FireWorking.Model;
using FireProtectionV1.MiniFireStationCore.Model;
using FireProtectionV1.User.Dto;
using FireProtectionV1.User.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.User.Manager
{
    public class FireUnitUserManager : DomainService, IFireUnitUserManager
    {
        IRepository<MiniFireStation> _repMiniFireStation;
        IRepository<FireUnitUser> _repFireUnitUser;
        IRepository<FireUnitUserRole> _fireUnitAccountRoleRepository;
        IRepository<FireUnit> _fireUnitRepository;
        IRepository<ShortMessageLog> _repShortMessageLog;
        ISqlRepository _SqlRepository;
        IRepository<FireUnitSystem> _fireUnitSystemRep;

        public FireUnitUserManager(
            IRepository<MiniFireStation> repMiniFireStation,
            IRepository<FireUnitSystem> fireUnitSystemRep,
            IRepository<FireUnitUser> repFireUnitUser,
            IRepository<FireUnitUserRole> fireUnitAccountRoleRepository,
            IRepository<FireUnit> fireUnitRepository,
            IRepository<ShortMessageLog> repShortMessageLog,
            ISqlRepository sqlRepository
            )
        {
            _repMiniFireStation = repMiniFireStation;
            _fireUnitSystemRep = fireUnitSystemRep;
            _repFireUnitUser = repFireUnitUser;
            _fireUnitAccountRoleRepository = fireUnitAccountRoleRepository;
            _fireUnitRepository = fireUnitRepository;
            _repShortMessageLog = repShortMessageLog;
            _SqlRepository = sqlRepository;
        }
        public async Task<SuccessOutput> UserRegist(UserRegistInput input)
        {
            var fireunit =await _fireUnitRepository.SingleAsync(p => p.Name.Equals(input.FireUnitName) && p.InvitationCode.Equals(input.InvitatCode));
            var user=_repFireUnitUser.GetAll().Where(p => p.Account.Equals(input.Phone)).FirstOrDefault();
            if (user != null)
                return new SuccessOutput() { Success = false, FailCause = "手机号已被注册" };
            string md5 = MD5Encrypt.Encrypt(input.Password + input.Phone, 16);
            int id=await _repFireUnitUser.InsertAndGetIdAsync(new FireUnitUser()
            {
                Account = input.Phone,
                Name = input.UserName,
                Status = Common.Enum.NormalStatus.Enabled,
                FireUnitID = fireunit.Id,
                Password = md5
            });
            FireUnitUserRole role = new FireUnitUserRole()
            {
                AccountID = id,
                Role = FireUnitRole.FireUnitManager
            };
            await _fireUnitAccountRoleRepository.InsertAsync(role);
            return new SuccessOutput() { Success = true };
        }
        /// <summary>
        /// 添加防火单位账号
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<int> Add(FireUnitUserInput input)
        {
            Valid.Exception(_repFireUnitUser.Count(m => m.Account.Equals(input.Account)) > 0, "手机号已被注册");
            var account = input.MapTo<FireUnitUser>();
            int accountID = await _repFireUnitUser.InsertAndGetIdAsync(account);

            var accountRole = new FireUnitUserRole()
            {
                AccountID = accountID,
                Role = input.Role
            };
            await _fireUnitAccountRoleRepository.InsertAsync(accountRole);
            return accountID;
        }
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<FireUnitUserLoginOutput> UserLogin(LoginInput input)
        {
            string md5 = MD5Encrypt.Encrypt(input.Password + input.Account, 16);
            FireUnitUserLoginOutput output = new FireUnitUserLoginOutput() { Success = true };
            var v = await _repFireUnitUser.FirstOrDefaultAsync(p => p.Account.Equals(input.Account) && p.Password.Equals(md5));
            if (v == null)
            {
                output.Success = false;
                output.FailCause = "账号或密码不正确";
            }
            else
            {
                output.UserId = v.Id;
                output.Name = v.Name;
                output.Account = v.Account;
                output.GuideFlage = false;
                var rolllist = _fireUnitAccountRoleRepository.GetAll();
                output.Rolelist = (from a in rolllist
                                   where a.AccountID == v.Id
                                   select a.Role).ToList();

                var fireunit = await _fireUnitRepository.FirstOrDefaultAsync(p => p.Id == v.FireUnitID);
                if (fireunit != null)
                {

                    output.ContractName = fireunit.ContractName;
                    output.ContractPhone = fireunit.ContractPhone;
                    output.FireUnitName = fireunit.Name;
                    output.FireUnitID = fireunit.Id;
                    var mini = await _repMiniFireStation.FirstOrDefaultAsync(p => p.FireUnitId == fireunit.Id);
                    if (mini != null)
                    {
                        output.MiniFireStationId = mini.Id;
                        output.MiniFireStationName = mini.Name;
                    }
                }
                if (output.Rolelist.Contains(FireUnitRole.FireUnitManager))
                {
                    output.GuideFlage = fireunit.Patrol == 0;
                    //因为引导是分步调用API，这里判断是否引导完成，也加条件判断
                    if (!output.GuideFlage)
                    {
                        output.GuideFlage = 0 == _fireUnitSystemRep.GetAll().Where(u => u.FireUnitId == v.FireUnitID).Count();
                    }
                }
            }
            return output;
        }
        /// <summary>
        /// 获取防火单位工作人员
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<GetUnitPeopleOutput>> GetFireUnitPeople(GetUnitPeopleInput input)
        {
            var loginman = await _repFireUnitUser.SingleAsync(u=>u.Id==input.AccountID);
            var unitpeoplelist = _repFireUnitUser.GetAll();
            var rolllist = _fireUnitAccountRoleRepository.GetAll();

            var unitpeople = from a in unitpeoplelist
                             where a.FireUnitID == loginman.FireUnitID
                             orderby a.CreationTime descending
                             let rolelst= rolllist.Where(u => u.AccountID == a.Id).Select(u => u.Role).ToList()
                             select new GetUnitPeopleOutput
                             {
                                 ID = a.Id,
                                 Name = a.Name,
                                 Account = a.Account,
                                 Rolelist = FireUnitRoleFunc.GetListName( rolelst),//rolllist.Where(u => u.AccountID == a.Id).Select(u => u.Role).ToList(),
                                 Photo=a.Photo,
                                 Qualification=a.Qualification,
                                 QualificationNumber=a.QualificationNumber,
                                 QualificationValidity=a.QualificationValidity != null ? ((DateTime)a.QualificationValidity).ToString("yyyy-MM-dd") : ""
                             };
            return unitpeople.ToList();
                
        }
        /// <summary>
        /// 获取工作人员详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetUnitPeopleOutput> GetUserInfo(GetUnitPeopleInput input)
        {
            var loginman = await _repFireUnitUser.SingleAsync(u => u.Id == input.AccountID);
            var rolllist = _fireUnitAccountRoleRepository.GetAll().Where(u => u.AccountID == input.AccountID).Select(u => u.Role).ToList();
            GetUnitPeopleOutput userInfo = new GetUnitPeopleOutput()
            {
                ID = loginman.Id,
                Name = loginman.Name,
                Account = loginman.Account,
                Rolelist = FireUnitRoleFunc.GetListName( rolllist),
                Photo = loginman.Photo,
                Qualification = loginman.Qualification,
                QualificationNumber = loginman.QualificationNumber,
                QualificationValidity = loginman.QualificationValidity != null ? ((DateTime)loginman.QualificationValidity).ToString("yyyy-MM-dd") : ""
            };
            return userInfo;

        }

        /// <summary>
        /// 编辑工作人员信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SuccessOutput> UpdateUserInfo(GetUnitPeopleOutput input)
        {
            SuccessOutput output = new SuccessOutput() { Success = true };
            var userInfo= await _repFireUnitUser.SingleAsync(u => u.Id == input.ID);
            userInfo.Account = input.Account;
            userInfo.Name = input.Name;
            userInfo.Photo = input.Photo;
            userInfo.Qualification = input.Qualification;
            userInfo.QualificationNumber = input.QualificationNumber;
            userInfo.QualificationValidity = DateTime.Parse( input.QualificationValidity);
            _repFireUnitUser.Update(userInfo);

            string sql = $@"DELETE FROM fireunituserrole WHERE AccountID={input.ID}";
            var dataTable = _SqlRepository.Query(sql);
            if (input.Rolelist != null)
            {
                foreach (var a in input.Rolelist)
                {
                    FireUnitUserRole userRole = new FireUnitUserRole()
                    {
                        AccountID = input.ID,
                        Role = FireUnitRoleFunc.GetRoleEnum(a)
                    };
                    _fireUnitAccountRoleRepository.Insert(userRole);
                }
            }

            return output;
        }
        /// <summary>
        /// 新增工作人员
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SuccessOutput> AddUser(AddUserInput input)
        {
            SuccessOutput output = new SuccessOutput() { Success = true };
            Valid.Exception(_repFireUnitUser.Count(m => m.Account.Equals(input.Account)) > 0, "手机号已被注册");
            FireUnitUser user = new FireUnitUser()
            {
                Name = input.Name,
                Account=input.Account,
                FireUnitID = input.FireUnitInfoID,
                Photo = input.Photo,
                Qualification = input.Qualification,
                QualificationNumber = input.QualificationNumber,
                Status = NormalStatus.Enabled,
                Password = MD5Encrypt.Encrypt("666666" + input.Account, 16),
            };
            if (!string.IsNullOrEmpty(input.QualificationValidity))
            {
                DateTime dt = DateTime.MinValue;
                DateTime.TryParse(input.QualificationValidity, out dt);
                user.QualificationValidity = dt;
            }
            var userid = _repFireUnitUser.InsertAndGetId(user);
            if (input.Rolelist != null)
            {
                // string roles = "";
                foreach (var roleid in input.Rolelist)
                {
                    if (!string.IsNullOrEmpty(roleid))
                    {
                        // roles += "," + roleid;
                        FireUnitUserRole role = new FireUnitUserRole()
                        {
                            AccountID = userid,
                            Role = FireUnitRoleFunc.GetRoleEnum(roleid)
                        };
                        await _fireUnitAccountRoleRepository.InsertAsync(role);
                    }
                }
                //if (!string.IsNullOrEmpty(roles))
                //{
                //    using (StreamWriter sw = new StreamWriter("log.txt", true))
                //    {
                //        sw.WriteLine(DateTime.Now.ToString("HH:mm:ss ffff") + $"，{roles.Substring(1)}");
                //        sw.WriteLine();
                //    }
                //}
            }
            return output;
        }

        /// <summary>
        /// 删除工作人员
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SuccessOutput> DeleteUser(DeleteUserInput input)
        {
            SuccessOutput output = new SuccessOutput() { Success = true };
            try {
                
                await _repFireUnitUser.DeleteAsync(u => u.Id == input.UserId);
            }
            catch(Exception e)
            {
                output.FailCause = e.Message;
                output.Success = false;
            }
            
            return output;
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SuccessOutput> ChangePassword(ChangeUserPassword input)
        {
            string md5 = MD5Encrypt.Encrypt(input.OldPassword + input.Account, 16);
            SuccessOutput output = new SuccessOutput() { Success = true };
            var v = await _repFireUnitUser.FirstOrDefaultAsync(p => p.Account.Equals(input.Account) && p.Password.Equals(md5));
            if (v == null)
            {
                output.Success = false;
                output.FailCause = "当前密码不正确";
            }
            else
            {
                string newMd5 = MD5Encrypt.Encrypt(input.NewPassword + input.Account, 16);
                v.Password = newMd5;
                var x = await _repFireUnitUser.UpdateAsync(v);
                output.Success = true;
            }
            return output;
        }
        /// <summary>
        /// 发送短信验证码
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public async Task<string> SendSMSCode(string phone)
        {
            var user = await _repFireUnitUser.FirstOrDefaultAsync(item => item.Account.Equals(phone));
            Valid.Exception(user == null, $"不存在手机号为{phone}的用户");

            Random rd = new Random();
            string verificationCode = rd.Next(3246, 9820).ToString();
            string content = $"尊敬的用户，您的验证码为：{verificationCode}。【天树聚智慧消防】";
            int result = await ShotMessageHelper.SendMessage(new Common.Helper.ShortMessage()
            {
                Phones = phone,
                Contents = content
            });

            await _repShortMessageLog.InsertAsync(new ShortMessageLog()
            {
                AlarmType = AlarmType.SMSCode,
                FireUnitId = user.FireUnitID,
                Phones = phone,
                Contents = content,
                Result = result
            });

            if (result.Equals(1))
            {
                return verificationCode;
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// 重置密码（忘记密码）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task ReSetPassword(ReSetPasswordInput input)
        {
            var user = await _repFireUnitUser.FirstOrDefaultAsync(item => item.Account.Equals(input.phone));
            Valid.Exception(user == null, $"不存在手机号为{input.phone}的用户");

            user.Password = MD5Encrypt.Encrypt(input.NewPassword + input.phone, 16);
            await _repFireUnitUser.UpdateAsync(user);
        }
    }
}
