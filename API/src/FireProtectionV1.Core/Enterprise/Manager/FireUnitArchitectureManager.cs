using Abp.Domain.Repositories;
using Abp.Domain.Services;
using FireProtectionV1.Common.DBContext;
using FireProtectionV1.Common.Helper;
using FireProtectionV1.Enterprise.Dto;
using FireProtectionV1.Enterprise.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.Enterprise.Manager
{
    public class FireUnitArchitectureManager : DomainService, IFireUnitArchitectureManager
    {
        IRepository<FireUnitArchitecture> _FireUnitArchitectureRepository;
        IRepository<FireUnitArchitectureFloor> _FireUnitArchitectureFloorRepository;

        public FireUnitArchitectureManager(
            IRepository<FireUnitArchitecture> fireUnitArchitectureRepository,
            IRepository<FireUnitArchitectureFloor> fireUnitArchitectureFloorRepository)
        {
            _FireUnitArchitectureRepository = fireUnitArchitectureRepository;
            _FireUnitArchitectureFloorRepository = fireUnitArchitectureFloorRepository;
        }

        public async Task Add(AddFireUnitArchitectureInput input)
        {
            Valid.Exception(_FireUnitArchitectureRepository.Count(m => m.FireUnitId.Equals(input.FireUnitId) && m.Name.Equals(input.Name)) > 0, "该建筑名称已存在");
            Valid.Exception(input.AboveNum == 0 && input.BelowNum == 0, "请填写建筑层数");

            int architectureId = await _FireUnitArchitectureRepository.InsertAndGetIdAsync(new FireUnitArchitecture()
            {
                CreationTime = DateTime.Now,
                Name = input.Name,
                AboveNum = input.AboveNum,
                BelowNum = input.BelowNum,
                BuildYear = input.BuildYear,
                Area = input.Area,
                Height = input.Height,
                Outward_Picture = input.Outward_Picture,
                FireUnitId = input.FireUnitId,
                FireDevice_LTJ_Exist = input.FireDevice_LTJ_Exist,
                FireDevice_LTJ_Detail = input.FireDevice_LTJ_Detail,
                FireDevice_HJ_Exist = input.FireDevice_HJ_Exist,
                FireDevice_HJ_Detail = input.FireDevice_HJ_Detail,
                FireDevice_MH_Exist = input.FireDevice_MH_Exist,
                FireDevice_MH_Detail = input.FireDevice_MH_Detail,
                FireDevice_TFPY_Exist = input.FireDevice_TFPY_Exist,
                FireDevice_TFPY_Detail = input.FireDevice_TFPY_Detail,
                FireDevice_XHS_Exist = input.FireDevice_XHS_Exist,
                FireDevice_XHS_Detail = input.FireDevice_XHS_Detail,
                FireDevice_XFSY_Exist = input.FireDevice_XFSY_Exist,
                FireDevice_XFSY_Detail = input.FireDevice_XFSY_Detail,
                FireDevice_FHM_Exist = input.FireDevice_FHM_Exist,
                FireDevice_FHM_Detail = input.FireDevice_FHM_Detail,
                FireDevice_FHJL_Exist = input.FireDevice_FHJL_Exist,
                FireDevice_FHJL_Detail = input.FireDevice_FHJL_Detail,
                FireDevice_MHQ_Exist = input.FireDevice_MHQ_Exist,
                FireDevice_MHQ_Detail = input.FireDevice_MHQ_Detail,
                FireDevice_YJZM_Exist = input.FireDevice_YJZM_Exist,
                FireDevice_YJZM_Detail = input.FireDevice_YJZM_Detail,
                FireDevice_SSZS_Exist = input.FireDevice_SSZS_Exist,
                FireDevice_SSZS_Detail = input.FireDevice_SSZS_Detail
            });

            for (int i = 1; i <= input.AboveNum; i++)
            {
                await _FireUnitArchitectureFloorRepository.InsertAsync(new FireUnitArchitectureFloor()
                {
                    CreationTime = DateTime.Now,
                    Name = i + "楼",
                    ArchitectureId = architectureId
                });
            }

            for (int i = 1; i <= input.BelowNum; i++)
            {
                await _FireUnitArchitectureFloorRepository.InsertAsync(new FireUnitArchitectureFloor()
                {
                    CreationTime = DateTime.Now,
                    Name = -i + "楼",
                    ArchitectureId = architectureId
                });
            }
        }

        public async Task Delete(int id)
        {
            await _FireUnitArchitectureRepository.DeleteAsync(id);
            await _FireUnitArchitectureFloorRepository.DeleteAsync(m => m.ArchitectureId.Equals(id));
        }

        public async Task<FireUnitArchitecture> GetById(int id)
        {
            return await _FireUnitArchitectureRepository.GetAsync(id);
        }

        public async Task Update(UpdateFireUnitArchitectureInput input)
        {
            Valid.Exception(_FireUnitArchitectureRepository.Count(m => m.FireUnitId.Equals(input.FireUnitId) && (input.Name.Equals(m.Name) && !input.Id.Equals(m.Id))) > 0, "保存失败：建筑名称已存在");
            Valid.Exception(input.AboveNum == 0 && input.BelowNum == 0, "请填写建筑层数");

            var old = _FireUnitArchitectureRepository.GetAll().Where(u => u.Id == input.Id).FirstOrDefault();
            if (old.AboveNum != input.AboveNum)
            {
                if (old.AboveNum > input.AboveNum)  // 改小了地上的楼层数
                {
                    for (int i = input.AboveNum + 1; i <= old.AboveNum; i++)
                    {
                        await _FireUnitArchitectureFloorRepository.DeleteAsync(m => m.ArchitectureId.Equals(input.Id) && m.Name.Equals(i + "楼"));
                    }
                }
                else  // 增加了地上的楼层数
                {
                    for (int i = old.AboveNum + 1; i <= input.AboveNum; i++)
                    {
                        await _FireUnitArchitectureFloorRepository.InsertAsync(new FireUnitArchitectureFloor()
                        {
                            CreationTime = DateTime.Now,
                            Name = i + "楼",
                            ArchitectureId = input.Id
                        });
                    }
                }
            }
            if (old.BelowNum != input.BelowNum)
            {
                if (old.BelowNum > input.BelowNum)  // 改小了地下的楼层数
                {
                    for (int i = input.BelowNum + 1; i <= old.BelowNum; i++)
                    {
                        await _FireUnitArchitectureFloorRepository.DeleteAsync(m => m.ArchitectureId.Equals(input.Id) && m.Name.Equals(-i + "楼"));
                    }
                }
                else  // 增加了地下的楼层数
                {
                    for (int i = old.BelowNum + 1; i <= input.BelowNum; i++)
                    {
                        await _FireUnitArchitectureFloorRepository.InsertAsync(new FireUnitArchitectureFloor()
                        {
                            CreationTime = DateTime.Now,
                            Name = -i + "楼",
                            ArchitectureId = input.Id
                        });
                    }
                }
            }

            old.Name = input.Name;
            old.AboveNum = input.AboveNum;
            old.BelowNum = input.BelowNum;
            old.BuildYear = input.BuildYear;
            old.Area = input.Area;
            old.Height = input.Height;
            old.Outward_Picture = input.Outward_Picture;
            old.FireDevice_LTJ_Exist = input.FireDevice_LTJ_Exist;
            old.FireDevice_LTJ_Detail = input.FireDevice_LTJ_Detail;
            old.FireDevice_HJ_Exist = input.FireDevice_HJ_Exist;
            old.FireDevice_HJ_Detail = input.FireDevice_HJ_Detail;
            old.FireDevice_MH_Exist = input.FireDevice_MH_Exist;
            old.FireDevice_MH_Detail = input.FireDevice_MH_Detail;
            old.FireDevice_TFPY_Exist = input.FireDevice_TFPY_Exist;
            old.FireDevice_TFPY_Detail = input.FireDevice_TFPY_Detail;
            old.FireDevice_XHS_Exist = input.FireDevice_XHS_Exist;
            old.FireDevice_XHS_Detail = input.FireDevice_XHS_Detail;
            old.FireDevice_XFSY_Exist = input.FireDevice_XFSY_Exist;
            old.FireDevice_XFSY_Detail = input.FireDevice_XFSY_Detail;
            old.FireDevice_FHM_Exist = input.FireDevice_FHM_Exist;
            old.FireDevice_FHM_Detail = input.FireDevice_FHM_Detail;
            old.FireDevice_FHJL_Exist = input.FireDevice_FHJL_Exist;
            old.FireDevice_FHJL_Detail = input.FireDevice_FHJL_Detail;
            old.FireDevice_MHQ_Exist = input.FireDevice_MHQ_Exist;
            old.FireDevice_MHQ_Detail = input.FireDevice_MHQ_Detail;
            old.FireDevice_YJZM_Exist = input.FireDevice_YJZM_Exist;
            old.FireDevice_YJZM_Detail = input.FireDevice_YJZM_Detail;
            old.FireDevice_SSZS_Exist = input.FireDevice_SSZS_Exist;
            old.FireDevice_SSZS_Detail = input.FireDevice_SSZS_Detail;

            await _FireUnitArchitectureRepository.UpdateAsync(old);
        }

        public async Task UpdateFloor(UpdateFireUnitArchitectureFloorInput input)
        {
            var old = _FireUnitArchitectureFloorRepository.GetAll().Where(u => u.Id == input.Id).FirstOrDefault();
            old.Floor_Picture = input.Floor_Picture;
            await _FireUnitArchitectureFloorRepository.UpdateAsync(old);
        }

        public Task<List<GetFireUnitArchitectureOutput>> GetListByFireUnitId(int fireunitId)
        {
            var FireUnitArchitectures = _FireUnitArchitectureRepository.GetAll();
            var FireUnitArchitectureFloors = _FireUnitArchitectureFloorRepository.GetAll();

            var expr = ExprExtension.True<FireUnitArchitecture>().And(item => item.FireUnitId.Equals(fireunitId));

            FireUnitArchitectures = FireUnitArchitectures.Where(expr);

            var query = from a in FireUnitArchitectures
                        select new GetFireUnitArchitectureOutput
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Floors = FireUnitArchitectureFloors.Where(item=>item.ArchitectureId.Equals(a.Id)).ToList()
                        };
            return Task.FromResult(query.ToList());
        }
    }
}
