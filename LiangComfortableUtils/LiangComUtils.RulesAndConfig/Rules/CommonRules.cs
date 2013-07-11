using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiangComUtils.RulesAndConfig.Rules
{
    class CommonRules
    {
        /// <summary>
        /// 根据公共交通工具类型获得名称
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetTransitNameByVehicleType(string type)
        {
            string result = string.Empty;
            if (!string.IsNullOrEmpty(type))
            {
                switch (type)
                {
                    case "RAIL":
                        result = "铁路";
                        break;
                    case "METRO_RAIL":
                        result = "轻轨交通";
                        break;
                    case "SUBWAY":
                        result = "地下轻轨";
                        break;
                    case "TRAM":
                        result = "地上轻轨";
                        break;
                    case "MONORAIL":
                        result = "单轨";
                        break;
                    case "HEAVY_RAIL":
                        result = "重轨";
                        break;
                    case "COMMUTER_TRAIN":
                        result = "通勤铁路";
                        break;
                    case "HIGH_SPEED_TRAIN":
                        result = "高速列车";
                        break;
                    case "BUS":
                        result = "公共汽车";
                        break;
                    case "INTERCITY_BUS":
                        result = "长途客车";
                        break;
                    case "TROLLEYBUS":
                        result = "无轨电车";
                        break;
                    case "SHARE_TAXI":
                        result = "合乘出租车";
                        break;
                    case "FERRY":
                        result = "渡船";
                        break;
                    case "CABLE_CAR":
                        result = "陆上缆车";
                        break;
                    case "GONDOLA_LIFT":
                        result = "空中缆车";
                        break;
                    case "FUNICULAR":
                        result = "索道缆车";
                        break;
                    case "OTHER":
                        result = "其他交通工具";
                        break;
                    default:
                        result = "未知交通工具";
                        break;
                }
            }
            return result;
        }
    }
}
