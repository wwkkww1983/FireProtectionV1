﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TsjDeviceServer
{
    class TsjTopic
    {
        string[] _topic;
        public TsjTopic(string topic)
        {
            if (topic != null)
                _topic = topic.Split('/');
        }
        public string Level(int level)
        {
            if (_topic == null)
                return "";
            else if (_topic.Length < level)
                return "";
            else
                return _topic[level - 1];
        }
    }
}
