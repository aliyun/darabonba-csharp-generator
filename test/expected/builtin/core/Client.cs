using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Darabonba;
using Darabonba.Utils;
using Darabonba.Streams;
using System.Linq;
using System.Globalization;
using Newtonsoft.Json;
using System.Text;

namespace Darabonba.Test
{
    public class Client 
    {

        public static void FormTest(List<string> args)
        {
            Dictionary<string, object> m = new Dictionary<string, object>
            {
                {"key1", "test1"},
                {"key2", "test2"},
                {"key3", 3},
                {"key4", new Dictionary<string, object>
                {
                    {"key5", 123},
                    {"key6", "321"},
                }},
            };
            string form = FormUtil.ToFormString(m);
            form = form + "&key7=23233&key8=" + FormUtil.GetBoundary();
            Stream r = FormUtil.ToFileForm(m, FormUtil.GetBoundary());
        }

        public static async Task FormTestAsync(List<string> args)
        {
            Dictionary<string, object> m = new Dictionary<string, object>
            {
                {"key1", "test1"},
                {"key2", "test2"},
                {"key3", 3},
                {"key4", new Dictionary<string, object>
                {
                    {"key5", 123},
                    {"key6", "321"},
                }},
            };
            string form = FormUtil.ToFormString(m);
            form = form + "&key7=23233&key8=" + FormUtil.GetBoundary();
            Stream r = FormUtil.ToFileForm(m, FormUtil.GetBoundary());
        }

        public static void FileTest(List<string> args)
        {
            if (DaraFile.Exists("/tmp/test"))
            {
                DaraFile file = new DaraFile("/tmp/test");
                string path = file.Path();
                int? length = file.Length() + 10;
                DaraDate createTime = file.CreateTime();
                DaraDate modifyTime = file.ModifyTime();
                int? timeLong = modifyTime.Diff("minute", createTime);
                byte[] data = file.Read(300);
                file.Write(BytesUtil.From("test", "utf8"));
                Stream rs = DaraFile.CreateReadStream("/tmp/test");
                Stream ws = DaraFile.CreateWriteStream("/tmp/test");
            }
        }

        public static async Task FileTestAsync(List<string> args)
        {
            if (DaraFile.Exists("/tmp/test"))
            {
                DaraFile file = new DaraFile("/tmp/test");
                string path = file.Path();
                int? length = await file.LengthAsync() + 10;
                DaraDate createTime = await file.CreateTimeAsync();
                DaraDate modifyTime = await file.ModifyTimeAsync();
                int? timeLong = modifyTime.Diff("minute", createTime);
                byte[] data = await file.ReadAsync(300);
                await file.WriteAsync(BytesUtil.From("test", "utf8"));
                Stream rs = DaraFile.CreateReadStream("/tmp/test");
                Stream ws = DaraFile.CreateWriteStream("/tmp/test");
            }
        }

        public static void DateTest(List<string> args)
        {
            DaraDate date = new DaraDate("2023-09-12 17:47:31.916000 +0800 UTC");
            string dateStr = date.Format("YYYY-MM-DD HH:mm:ss");
            int? timestamp = date.Unix();
            DaraDate yesterday = date.Sub("day", 1);
            int? oneDay = date.Diff("day", yesterday);
            DaraDate tomorrow = date.Add("day", 1);
            int? twoDay = tomorrow.Diff("day", date) + oneDay;
            int? hour = date.Hour();
            int? minute = date.Minute();
            int? second = date.Second();
            int? dayOfMonth = date.DayOfMonth();
            int? dayOfWeek = date.DayOfWeek();
            // var weekOfMonth = date.weekOfMonth();
            int? weekOfYear = date.WeekOfYear();
            int? month = date.Month();
            int? year = date.Year();
        }

        public static async Task DateTestAsync(List<string> args)
        {
            DaraDate date = new DaraDate("2023-09-12 17:47:31.916000 +0800 UTC");
            string dateStr = date.Format("YYYY-MM-DD HH:mm:ss");
            int? timestamp = date.Unix();
            DaraDate yesterday = date.Sub("day", 1);
            int? oneDay = date.Diff("day", yesterday);
            DaraDate tomorrow = date.Add("day", 1);
            int? twoDay = tomorrow.Diff("day", date) + oneDay;
            int? hour = date.Hour();
            int? minute = date.Minute();
            int? second = date.Second();
            int? dayOfMonth = date.DayOfMonth();
            int? dayOfWeek = date.DayOfWeek();
            // var weekOfMonth = date.weekOfMonth();
            int? weekOfYear = date.WeekOfYear();
            int? month = date.Month();
            int? year = date.Year();
        }

        public static void UrlTest(List<string> args)
        {
            DaraURL url = new DaraURL(args[0]);
            string path = url.Path();
            string pathname = url.Pathname();
            string protocol = url.Protocol();
            string hostname = url.Hostname();
            string port = url.Port();
            string host = url.Host();
            string hash = url.Hash();
            string search = url.Search();
            string href = url.Href();
            string auth = url.Auth();
            DaraURL url2 = DaraURL.Parse(args[1]);
            path = url2.Path();
            string newUrl = DaraURL.UrlEncode(args[2]);
            string newSearch = DaraURL.PercentEncode(search);
            string newPath = DaraURL.PathEncode(pathname);
            string all = "test" + path + protocol + hostname + hash + search + href + auth + newUrl + newSearch + newPath;
        }

        public static async Task UrlTestAsync(List<string> args)
        {
            DaraURL url = new DaraURL(args[0]);
            string path = url.Path();
            string pathname = url.Pathname();
            string protocol = url.Protocol();
            string hostname = url.Hostname();
            string port = url.Port();
            string host = url.Host();
            string hash = url.Hash();
            string search = url.Search();
            string href = url.Href();
            string auth = url.Auth();
            DaraURL url2 = DaraURL.Parse(args[1]);
            path = url2.Path();
            string newUrl = DaraURL.UrlEncode(args[2]);
            string newSearch = DaraURL.PercentEncode(search);
            string newPath = DaraURL.PathEncode(pathname);
            string all = "test" + path + protocol + hostname + hash + search + href + auth + newUrl + newSearch + newPath;
        }

        public static void StreamTest(List<string> args)
        {
            if (DaraFile.Exists("/tmp/test"))
            {
                Stream rs = DaraFile.CreateReadStream("/tmp/test");
                Stream ws = DaraFile.CreateWriteStream("/tmp/test");
                byte[] data = StreamUtil.Read(rs, 30);
                ws.Write(data, 0, data.Length);
                StreamUtil.Pipe(rs, ws);
                data = StreamUtil.ReadAsBytes(rs);
                object obj = StreamUtil.ReadAsJSON(rs);
                string jsonStr = StreamUtil.ReadAsString(rs);
            }
        }

        public static async Task StreamTestAsync(List<string> args)
        {
            if (DaraFile.Exists("/tmp/test"))
            {
                Stream rs = DaraFile.CreateReadStream("/tmp/test");
                Stream ws = DaraFile.CreateWriteStream("/tmp/test");
                byte[] data = StreamUtil.Read(rs, 30);
                ws.Write(data, 0, data.Length);
                StreamUtil.Pipe(rs, ws);
                data = StreamUtil.ReadAsBytes(rs);
                object obj = StreamUtil.ReadAsJSON(rs);
                string jsonStr = StreamUtil.ReadAsString(rs);
            }
        }

        public static void XmlTest(List<string> args)
        {
            Dictionary<string, object> m = new Dictionary<string, object>
            {
                {"key1", "test1"},
                {"key2", "test2"},
                {"key3", 3},
                {"key4", new Dictionary<string, object>
                {
                    {"key5", 123},
                    {"key6", "321"},
                }},
            };
            string xml = XmlUtil.ToXML(m);
            xml = xml + "<key7>132</key7>";
            Dictionary<string, object> respMap = XmlUtil.ParseXml(xml, null);
        }

        public static async Task XmlTestAsync(List<string> args)
        {
            Dictionary<string, object> m = new Dictionary<string, object>
            {
                {"key1", "test1"},
                {"key2", "test2"},
                {"key3", 3},
                {"key4", new Dictionary<string, object>
                {
                    {"key5", 123},
                    {"key6", "321"},
                }},
            };
            string xml = XmlUtil.ToXML(m);
            xml = xml + "<key7>132</key7>";
            Dictionary<string, object> respMap = XmlUtil.ParseXml(xml, null);
        }

        public static void LoggerTest(List<string> args)
        {
            Console.WriteLine("test");
            Console.WriteLine("test");
            Console.WriteLine("test");
            Console.WriteLine("test");
            Console.Error.WriteLine("test");
        }

        public static async Task LoggerTestAsync(List<string> args)
        {
            Console.WriteLine("test");
            Console.WriteLine("test");
            Console.WriteLine("test");
            Console.WriteLine("test");
            Console.Error.WriteLine("test");
        }

        public static void EnvTest(List<string> args)
        {
            string es = Environment.GetEnvironmentVariable("TEST");
            Environment.SetEnvironmentVariable("TEST", es + "test");
        }

        public static async Task EnvTestAsync(List<string> args)
        {
            string es = Environment.GetEnvironmentVariable("TEST");
            Environment.SetEnvironmentVariable("TEST", es + "test");
        }

        public static void NumberTest(List<string> args)
        {
            float? num = 3.2f;
            int? inum = MathUtil.ParseInt(num);
            long? lnum = MathUtil.ParseLong(num);
            float? fnum = MathUtil.ParseFloat(num);
            double? dnum = Double.Parse(num.Value.ToString());
            inum = MathUtil.ParseInt(inum);
            lnum = MathUtil.ParseLong(inum);
            fnum = MathUtil.ParseFloat(inum);
            dnum = Double.Parse(inum.Value.ToString());
            inum = MathUtil.ParseInt(lnum);
            lnum = MathUtil.ParseLong(lnum);
            fnum = MathUtil.ParseFloat(lnum);
            dnum = Double.Parse(lnum.Value.ToString());
            inum = MathUtil.ParseInt(fnum);
            lnum = MathUtil.ParseLong(fnum);
            fnum = MathUtil.ParseFloat(fnum);
            dnum = Double.Parse(fnum.Value.ToString());
            inum = MathUtil.ParseInt(dnum);
            lnum = MathUtil.ParseLong(dnum);
            fnum = MathUtil.ParseFloat(dnum);
            dnum = Double.Parse(dnum.Value.ToString());
            lnum = inum;
            inum = (int)lnum.Value;
            double randomNum = new Random().NextDouble();
            inum = MathUtil.Floor(inum);
            inum = MathUtil.Round(inum);
            // var min = $Number.min(inum, fnum);
            // var max = $Number.max(inum, fnum);
        }

        public static async Task NumberTestAsync(List<string> args)
        {
            float? num = 3.2f;
            int? inum = MathUtil.ParseInt(num);
            long? lnum = MathUtil.ParseLong(num);
            float? fnum = MathUtil.ParseFloat(num);
            double? dnum = Double.Parse(num.Value.ToString());
            inum = MathUtil.ParseInt(inum);
            lnum = MathUtil.ParseLong(inum);
            fnum = MathUtil.ParseFloat(inum);
            dnum = Double.Parse(inum.Value.ToString());
            inum = MathUtil.ParseInt(lnum);
            lnum = MathUtil.ParseLong(lnum);
            fnum = MathUtil.ParseFloat(lnum);
            dnum = Double.Parse(lnum.Value.ToString());
            inum = MathUtil.ParseInt(fnum);
            lnum = MathUtil.ParseLong(fnum);
            fnum = MathUtil.ParseFloat(fnum);
            dnum = Double.Parse(fnum.Value.ToString());
            inum = MathUtil.ParseInt(dnum);
            lnum = MathUtil.ParseLong(dnum);
            fnum = MathUtil.ParseFloat(dnum);
            dnum = Double.Parse(dnum.Value.ToString());
            lnum = inum;
            inum = (int)lnum.Value;
            double randomNum = new Random().NextDouble();
            inum = MathUtil.Floor(inum);
            inum = MathUtil.Round(inum);
            // var min = $Number.min(inum, fnum);
            // var max = $Number.max(inum, fnum);
        }

        public static void StringTest(List<string> args)
        {
            string fullStr = string.Join(",", args);
            args = fullStr.Split(",").ToList();
            if ((fullStr.Length > 0) && fullStr.Contains("hangzhou"))
            {
                string newStr1 = StringUtil.Replace(fullStr, "/hangzhou/g", "beijing");
            }
            if (fullStr.StartsWith("cn"))
            {
                string newStr2 = StringUtil.Replace(fullStr, "/cn/gi", "zh");
            }
            if (fullStr.StartsWith("beijing"))
            {
                string newStr3 = StringUtil.Replace(fullStr, "/beijing/", "chengdu");
            }
            int? start = fullStr.IndexOf("beijing");
            int? end = start + 7;
            string region = StringUtil.SubString(fullStr, start, end);
            string region1 = StringUtil.SubString(fullStr, 2, 10);
            string lowerRegion = region.ToLower();
            string upperRegion = region.ToUpper();
            if (region == "beijing")
            {
                region = region + " ";
                region = region.Trim();
            }
            byte[] tb = StringUtil.ToBytes(fullStr, "utf8");
            string em = "xxx";
            if (String.IsNullOrEmpty(em))
            {
                return ;
            }
            string num = "32.01";
            int? inum = StringUtil.ParseInt(num) + 3;
            long? lnum = StringUtil.ParseLong(num);
            float? fnum = StringUtil.ParseFloat(num) + 1f;
            double? dnum = Double.Parse(num, NumberStyles.Float | NumberStyles.AllowThousands, NumberFormatInfo.InvariantInfo) + 1;
        }

        public static async Task StringTestAsync(List<string> args)
        {
            string fullStr = string.Join(",", args);
            args = fullStr.Split(",").ToList();
            if ((fullStr.Length > 0) && fullStr.Contains("hangzhou"))
            {
                string newStr1 = StringUtil.Replace(fullStr, "/hangzhou/g", "beijing");
            }
            if (fullStr.StartsWith("cn"))
            {
                string newStr2 = StringUtil.Replace(fullStr, "/cn/gi", "zh");
            }
            if (fullStr.StartsWith("beijing"))
            {
                string newStr3 = StringUtil.Replace(fullStr, "/beijing/", "chengdu");
            }
            int? start = fullStr.IndexOf("beijing");
            int? end = start + 7;
            string region = StringUtil.SubString(fullStr, start, end);
            string region1 = StringUtil.SubString(fullStr, 2, 10);
            string lowerRegion = region.ToLower();
            string upperRegion = region.ToUpper();
            if (region == "beijing")
            {
                region = region + " ";
                region = region.Trim();
            }
            byte[] tb = StringUtil.ToBytes(fullStr, "utf8");
            string em = "xxx";
            if (String.IsNullOrEmpty(em))
            {
                return ;
            }
            string num = "32.01";
            int? inum = StringUtil.ParseInt(num) + 3;
            long? lnum = StringUtil.ParseLong(num);
            float? fnum = StringUtil.ParseFloat(num) + 1f;
            double? dnum = Double.Parse(num, NumberStyles.Float | NumberStyles.AllowThousands, NumberFormatInfo.InvariantInfo) + 1;
        }

        public static void ArrayTest(List<string> args)
        {
            if ((args.Count > 0) && args.Contains("cn-hanghzou"))
            {
                int? index = args.IndexOf("cn-hanghzou");
                string regionId = args[index.Value];
                string all = string.Join(",", args);
                string first = ListUtil.Shift(args);
                string last = ListUtil.Pop(args);
                int? length1 = ListUtil.Unshift(args, first);
                int? length2 = ListUtil.Push(args, last);
                int? length3 = length1 + length2;
                string longStr = "long" + first + last;
                string fullStr = string.Join(",", args);
                List<string> newArr = new List<string>
                {
                    "test"
                };
                List<string> cArr = ListUtil.Concat(newArr, args);
                List<string> acsArr = ListUtil.Sort(newArr, "acs");
                List<string> descArr = ListUtil.Sort(newArr, "desc");
                List<string> llArr = ListUtil.Concat(acsArr, descArr);
                llArr.Insert(10, "test");
                llArr.RemoveAt(llArr.IndexOf("test"));
            }
        }

        public static async Task ArrayTestAsync(List<string> args)
        {
            if ((args.Count > 0) && args.Contains("cn-hanghzou"))
            {
                int? index = args.IndexOf("cn-hanghzou");
                string regionId = args[index.Value];
                string all = string.Join(",", args);
                string first = ListUtil.Shift(args);
                string last = ListUtil.Pop(args);
                int? length1 = ListUtil.Unshift(args, first);
                int? length2 = ListUtil.Push(args, last);
                int? length3 = length1 + length2;
                string longStr = "long" + first + last;
                string fullStr = string.Join(",", args);
                List<string> newArr = new List<string>
                {
                    "test"
                };
                List<string> cArr = ListUtil.Concat(newArr, args);
                List<string> acsArr = ListUtil.Sort(newArr, "acs");
                List<string> descArr = ListUtil.Sort(newArr, "desc");
                List<string> llArr = ListUtil.Concat(acsArr, descArr);
                llArr.Insert(10, "test");
                llArr.RemoveAt(llArr.IndexOf("test"));
            }
        }

        public static void JsonTest(List<string> args)
        {
            Dictionary<string, object> m = new Dictionary<string, object>
            {
                {"key1", "test1"},
                {"key2", "test2"},
                {"key3", 3},
                {"key4", new Dictionary<string, object>
                {
                    {"key5", 123},
                    {"key6", "321"},
                }},
            };
            DaraCore.Sleep(10);
            string ms = JSONUtil.SerializeObject(m);
            object ma = JsonConvert.DeserializeObject(ms);
            string arrStr = "[1,2,3,4]";
            object arr = JsonConvert.DeserializeObject(arrStr);
        }

        public static async Task JsonTestAsync(List<string> args)
        {
            Dictionary<string, object> m = new Dictionary<string, object>
            {
                {"key1", "test1"},
                {"key2", "test2"},
                {"key3", 3},
                {"key4", new Dictionary<string, object>
                {
                    {"key5", 123},
                    {"key6", "321"},
                }},
            };
            await DaraCore.SleepAsync(10);
            string ms = JSONUtil.SerializeObject(m);
            object ma = JsonConvert.DeserializeObject(ms);
            string arrStr = "[1,2,3,4]";
            object arr = JsonConvert.DeserializeObject(arrStr);
        }

        public static object ReturnAny()
        {
            throw new NotImplementedException();
        }

        public static void Main(string[] args)
        {
            int? a = ConverterUtil.ParseInt(args[0]) + 10;
            string b = (string)a + args[1] + (string)ReturnAny();
            int? c = ConverterUtil.ParseInt(b) + ConverterUtil.ParseInt(a) + ConverterUtil.ParseInt(ReturnAny());
            int? d = ConverterUtil.ParseInt(b) + ConverterUtil.ParseInt(a) + ConverterUtil.ParseInt(ReturnAny());
            int? e = ConverterUtil.ParseInt(b) + ConverterUtil.ParseInt(a) + ConverterUtil.ParseInt(ReturnAny());
            int? f = ConverterUtil.ParseInt(b) + ConverterUtil.ParseInt(a) + ConverterUtil.ParseInt(ReturnAny());
            long? g = ConverterUtil.ParseLong(b) + ConverterUtil.ParseLong(a) + ConverterUtil.ParseLong(ReturnAny());
            long? h = ConverterUtil.ParseLong(b) + ConverterUtil.ParseLong(a) + ConverterUtil.ParseLong(ReturnAny());
            ulong? i = ConverterUtil.ParseLong(b) + ConverterUtil.ParseLong(a) + ConverterUtil.ParseLong(ReturnAny());
            uint? j = ConverterUtil.ParseInt(b) + ConverterUtil.ParseInt(a) + ConverterUtil.ParseInt(ReturnAny());
            uint? k = ConverterUtil.ParseInt(b) + ConverterUtil.ParseInt(a) + ConverterUtil.ParseInt(ReturnAny());
            uint? l = ConverterUtil.ParseInt(b) + ConverterUtil.ParseInt(a) + ConverterUtil.ParseInt(ReturnAny());
            ulong? m = ConverterUtil.ParseLong(b) + ConverterUtil.ParseLong(a) + ConverterUtil.ParseLong(ReturnAny());
            float? n = ConverterUtil.ParseFloat(b) + ConverterUtil.ParseFloat(a) + ConverterUtil.ParseFloat(ReturnAny());
            double? o = Double.Parse(b.ToString()) + Double.Parse(a.ToString()) + Double.Parse(ReturnAny().ToString());
            if ((bool)(args[2]))
            {
                // bytes 强转只允许传字符串
                byte[] data = (byte[])(b);
                int? length = data.Length;
                object test = data;
                Dictionary<string, string> maps = new Dictionary<string, string>
                {
                    {"key", "value"},
                };
                Dictionary<string, object> obj = DaraCore.ToObject(maps);
                Stream ws = (Stream)obj;
                Stream rs = ConverterUtil.ToStream(maps);
                data = StreamUtil.Read(rs, 30);
                if (!data.IsNull())
                {
                    ws.Write(data, 0, data.Length);
                }
            }
            DaraCore.Sleep(a.Value);
            string defaultVal = (string)(args[0] ?? args[1]);
            if (defaultVal == b)
            {
                return ;
            }
        }

        public static void BytesTest(List<string> args)
        {
            string fullStr = string.Join(",", args);
            byte[] data = StringUtil.ToBytes(fullStr, "utf8");
            string newFullStr = Encoding.UTF8.GetString(data);
            if (fullStr != newFullStr)
            {
                return ;
            }
            string hexStr = BytesUtil.ToHex(data);
            string base64Str = Convert.ToBase64String(data);
            int? length = data.Length;
            string obj = Encoding.UTF8.GetString(data);
            byte[] data2 = BytesUtil.From(base64Str, "base64");
        }

        public static async Task BytesTestAsync(List<string> args)
        {
            string fullStr = string.Join(",", args);
            byte[] data = StringUtil.ToBytes(fullStr, "utf8");
            string newFullStr = Encoding.UTF8.GetString(data);
            if (fullStr != newFullStr)
            {
                return ;
            }
            string hexStr = BytesUtil.ToHex(data);
            string base64Str = Convert.ToBase64String(data);
            int? length = data.Length;
            string obj = Encoding.UTF8.GetString(data);
            byte[] data2 = BytesUtil.From(base64Str, "base64");
        }

        public static void MapTestCase(List<string> args)
        {
            Dictionary<string, string> mapTest = new Dictionary<string, string>
            {
                {"key1", "value1"},
                {"key2", "value2"},
                {"key3", "value3"},
            };
            int? length = mapTest.Count;
            int? num = length + 3;
            List<string> keys = mapTest.Keys.ToList();
            string allKey = "";

            foreach (var key in keys) {
                allKey = allKey + key;
            }
            List<KeyValuePair<string, string>> entries = mapTest.ToList();
            string newKey = "";
            string newValue = "";

            foreach (var e in entries) {
                newKey = newKey + e.Key;
                newValue = newValue + e.Value;
            }
            string json = JsonConvert.SerializeObject(mapTest);
            Dictionary<string, string> mapTest2 = new Dictionary<string, string>
            {
                {"key1", "value4"},
                {"key4", "value5"},
            };
            Dictionary<string, object> mapTest3 = ConverterUtil.Merge(mapTest , mapTest2);
            if (mapTest3.Get("key1") == "value4")
            {
                return ;
            }
        }

        public static async Task MapTestCaseAsync(List<string> args)
        {
            Dictionary<string, string> mapTest = new Dictionary<string, string>
            {
                {"key1", "value1"},
                {"key2", "value2"},
                {"key3", "value3"},
            };
            int? length = mapTest.Count;
            int? num = length + 3;
            List<string> keys = mapTest.Keys.ToList();
            string allKey = "";

            foreach (var key in keys) {
                allKey = allKey + key;
            }
            List<KeyValuePair<string, string>> entries = mapTest.ToList();
            string newKey = "";
            string newValue = "";

            foreach (var e in entries) {
                newKey = newKey + e.Key;
                newValue = newValue + e.Value;
            }
            string json = JsonConvert.SerializeObject(mapTest);
            Dictionary<string, string> mapTest2 = new Dictionary<string, string>
            {
                {"key1", "value4"},
                {"key4", "value5"},
            };
            Dictionary<string, object> mapTest3 = ConverterUtil.Merge(mapTest , mapTest2);
            if (mapTest3.Get("key1") == "value4")
            {
                return ;
            }
        }

    }
}

