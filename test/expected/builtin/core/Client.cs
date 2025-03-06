using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Darabonba;
using Darabonba.Utils;
using System.Linq;
using System.Threading;
using Newtonsoft.Json;
using System.Text;
using Darabonba.Test.Models;

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
            string form = FormUtils.ToFormString(m);
            form = form + "&key7=23233&key8=" + FormUtils.GetBoundary();
            Stream r = FormUtils.ToFileForm(m, FormUtils.GetBoundary());
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
            string form = FormUtils.ToFormString(m);
            form = form + "&key7=23233&key8=" + FormUtils.GetBoundary();
            Stream r = FormUtils.ToFileForm(m, FormUtils.GetBoundary());
        }

        public static void FileTest(List<string> args)
        {
            if (File.Exists("/tmp/test"))
            {
                File file = new File("/tmp/test");
                string path = file.Path();
                int? length = file.Length() + 10;
                Date createTime = file.CreateTime();
                Date modifyTime = file.ModifyTime();
                int? timeLong = modifyTime.Diff("minute", createTime);
                byte[] data = file.Read(300);
                file.Write(BytesUtils.From("test", "utf8"));
                Stream rs = File.CreateReadStream("/tmp/test");
                Stream ws = File.CreateWriteStream("/tmp/test");
            }
        }

        public static async Task FileTestAsync(List<string> args)
        {
            if (File.Exists("/tmp/test"))
            {
                File file = new File("/tmp/test");
                string path = file.Path();
                int? length = await file.LengthAsync() + 10;
                Date createTime = await file.CreateTimeAsync();
                Date modifyTime = await file.ModifyTimeAsync();
                int? timeLong = modifyTime.Diff("minute", createTime);
                byte[] data = await file.ReadAsync(300);
                await file.WriteAsync(BytesUtils.From("test", "utf8"));
                Stream rs = File.CreateReadStream("/tmp/test");
                Stream ws = File.CreateWriteStream("/tmp/test");
            }
        }

        public static void DateTest(List<string> args)
        {
            Date date = new Date("2023-09-12 17:47:31.916000 +0800 UTC");
            string dateStr = date.Format("YYYY-MM-DD HH:mm:ss");
            int? timestamp = date.Unix();
            Date yesterday = date.Sub("day", 1);
            int? oneDay = date.Diff("day", yesterday);
            Date tomorrow = date.Add("day", 1);
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
            Date date = new Date("2023-09-12 17:47:31.916000 +0800 UTC");
            string dateStr = date.Format("YYYY-MM-DD HH:mm:ss");
            int? timestamp = date.Unix();
            Date yesterday = date.Sub("day", 1);
            int? oneDay = date.Diff("day", yesterday);
            Date tomorrow = date.Add("day", 1);
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
            URL url = new URL(args[0]);
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
            URL url2 = URL.Parse(args[1]);
            path = url2.Path();
            string newUrl = URL.UrlEncode(args[2]);
            string newSearch = URL.PercentEncode(search);
            string newPath = URL.PathEncode(pathname);
            string all = "test" + path + protocol + hostname + hash + search + href + auth + newUrl + newSearch + newPath;
        }

        public static async Task UrlTestAsync(List<string> args)
        {
            URL url = new URL(args[0]);
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
            URL url2 = URL.Parse(args[1]);
            path = url2.Path();
            string newUrl = URL.UrlEncode(args[2]);
            string newSearch = URL.PercentEncode(search);
            string newPath = URL.PathEncode(pathname);
            string all = "test" + path + protocol + hostname + hash + search + href + auth + newUrl + newSearch + newPath;
        }

        public static void StreamTest(List<string> args)
        {
            if (File.Exists("/tmp/test"))
            {
                Stream rs = File.CreateReadStream("/tmp/test");
                Stream ws = File.CreateWriteStream("/tmp/test");
                byte[] data = StreamUtils.Read(rs, 30);
                ws.Write(data, 0, data.Length);
                StreamUtils.Pipe(rs, ws);
                data = StreamUtils.ReadAsBytes(rs);
                object obj = StreamUtils.ReadAsJSON(rs);
                string jsonStr = StreamUtils.ReadAsString(rs);
            }
        }

        public static async Task StreamTestAsync(List<string> args)
        {
            if (File.Exists("/tmp/test"))
            {
                Stream rs = File.CreateReadStream("/tmp/test");
                Stream ws = File.CreateWriteStream("/tmp/test");
                byte[] data = StreamUtils.Read(rs, 30);
                ws.Write(data, 0, data.Length);
                StreamUtils.Pipe(rs, ws);
                data = StreamUtils.ReadAsBytes(rs);
                object obj = StreamUtils.ReadAsJSON(rs);
                string jsonStr = StreamUtils.ReadAsString(rs);
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
            string xml = XmlUtils.ToXML(m);
            xml = xml + "<key7>132</key7>";
            Dictionary<string, object> respMap = XmlUtils.ParseXml(xml, null);
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
            string xml = XmlUtils.ToXML(m);
            xml = xml + "<key7>132</key7>";
            Dictionary<string, object> respMap = XmlUtils.ParseXml(xml, null);
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
            int? inum = MathUtils.ParseInt(num);
            long? lnum = MathUtils.ParseLong(num);
            float? fnum = MathUtils.ParseFloat(num);
            double? dnum = double.Parse(num.Value.ToString());
            inum = MathUtils.ParseInt(inum);
            lnum = MathUtils.ParseLong(inum);
            fnum = MathUtils.ParseFloat(inum);
            dnum = double.Parse(inum.Value.ToString());
            inum = MathUtils.ParseInt(lnum);
            lnum = MathUtils.ParseLong(lnum);
            fnum = MathUtils.ParseFloat(lnum);
            dnum = double.Parse(lnum.Value.ToString());
            inum = MathUtils.ParseInt(fnum);
            lnum = MathUtils.ParseLong(fnum);
            fnum = MathUtils.ParseFloat(fnum);
            dnum = double.Parse(fnum.Value.ToString());
            inum = MathUtils.ParseInt(dnum);
            lnum = MathUtils.ParseLong(dnum);
            fnum = MathUtils.ParseFloat(dnum);
            dnum = double.Parse(dnum.Value.ToString());
            lnum = inum;
            inum = (int?)lnum.Value;
            double randomNum = new Random().NextDouble();
            inum = MathUtils.Floor(inum);
            inum = MathUtils.Round(inum);
            // var min = $Number.min(inum, fnum);
            // var max = $Number.max(inum, fnum);
        }

        public static async Task NumberTestAsync(List<string> args)
        {
            float? num = 3.2f;
            int? inum = MathUtils.ParseInt(num);
            long? lnum = MathUtils.ParseLong(num);
            float? fnum = MathUtils.ParseFloat(num);
            double? dnum = double.Parse(num.Value.ToString());
            inum = MathUtils.ParseInt(inum);
            lnum = MathUtils.ParseLong(inum);
            fnum = MathUtils.ParseFloat(inum);
            dnum = double.Parse(inum.Value.ToString());
            inum = MathUtils.ParseInt(lnum);
            lnum = MathUtils.ParseLong(lnum);
            fnum = MathUtils.ParseFloat(lnum);
            dnum = double.Parse(lnum.Value.ToString());
            inum = MathUtils.ParseInt(fnum);
            lnum = MathUtils.ParseLong(fnum);
            fnum = MathUtils.ParseFloat(fnum);
            dnum = double.Parse(fnum.Value.ToString());
            inum = MathUtils.ParseInt(dnum);
            lnum = MathUtils.ParseLong(dnum);
            fnum = MathUtils.ParseFloat(dnum);
            dnum = double.Parse(dnum.Value.ToString());
            lnum = inum;
            inum = (int?)lnum.Value;
            double randomNum = new Random().NextDouble();
            inum = MathUtils.Floor(inum);
            inum = MathUtils.Round(inum);
            // var min = $Number.min(inum, fnum);
            // var max = $Number.max(inum, fnum);
        }

        public static void StringTest(List<string> args)
        {
            string fullStr = string.Join(",", args);
            args = fullStr.Split(",").ToList();
            if ((fullStr.Length > 0) && fullStr.Contains("hangzhou"))
            {
                string newStr1 = StringUtils.Replace(fullStr, "/hangzhou/g", "beijing");
            }
            if (fullStr.StartsWith("cn"))
            {
                string newStr2 = StringUtils.Replace(fullStr, "/cn/gi", "zh");
            }
            if (fullStr.EndsWith("beijing"))
            {
                string newStr3 = StringUtils.Replace(fullStr, "/beijing/", "chengdu");
            }
            int? start = fullStr.IndexOf("beijing");
            int? end = start + 7;
            string region = StringUtils.SubString(fullStr, start, end);
            string region1 = StringUtils.SubString(fullStr, 2, 10);
            string lowerRegion = region.ToLower();
            string upperRegion = region.ToUpper();
            if (region == "beijing")
            {
                region = region + " ";
                region = region.Trim();
            }
            byte[] tb = StringUtils.ToBytes(fullStr, "utf8");
            string em = "xxx";
            if (String.IsNullOrEmpty(em))
            {
                return ;
            }
            string num = "32.01";
            int? inum = int.Parse(num) + 3;
            long? lnum = long.Parse(num);
            float? fnum = float.Parse(num) + 1f;
            double? dnum = double.Parse(num) + 1;
        }

        public static async Task StringTestAsync(List<string> args)
        {
            string fullStr = string.Join(",", args);
            args = fullStr.Split(",").ToList();
            if ((fullStr.Length > 0) && fullStr.Contains("hangzhou"))
            {
                string newStr1 = StringUtils.Replace(fullStr, "/hangzhou/g", "beijing");
            }
            if (fullStr.StartsWith("cn"))
            {
                string newStr2 = StringUtils.Replace(fullStr, "/cn/gi", "zh");
            }
            if (fullStr.EndsWith("beijing"))
            {
                string newStr3 = StringUtils.Replace(fullStr, "/beijing/", "chengdu");
            }
            int? start = fullStr.IndexOf("beijing");
            int? end = start + 7;
            string region = StringUtils.SubString(fullStr, start, end);
            string region1 = StringUtils.SubString(fullStr, 2, 10);
            string lowerRegion = region.ToLower();
            string upperRegion = region.ToUpper();
            if (region == "beijing")
            {
                region = region + " ";
                region = region.Trim();
            }
            byte[] tb = StringUtils.ToBytes(fullStr, "utf8");
            string em = "xxx";
            if (String.IsNullOrEmpty(em))
            {
                return ;
            }
            string num = "32.01";
            int? inum = int.Parse(num) + 3;
            long? lnum = long.Parse(num);
            float? fnum = float.Parse(num) + 1f;
            double? dnum = double.Parse(num) + 1;
        }

        public static void ArrayTest(List<string> args)
        {
            if ((args.Count > 0) && args.Contains("cn-hanghzou"))
            {
                int? index = args.IndexOf("cn-hanghzou");
                string regionId = args[index.Value];
                string all = string.Join(",", args);
                string first = ListUtils.Shift(args);
                string last = ListUtils.Pop(args);
                int? length1 = ListUtils.Unshift(args, first);
                int? length2 = ListUtils.Push(args, last);
                int? length3 = length1 + length2;
                string longStr = "long" + first + last;
                string fullStr = string.Join(",", args);
                List<string> newArr = new List<string>
                {
                    "test"
                };
                List<string> cArr = ListUtils.Concat(newArr, args);
                List<string> acsArr = ListUtils.Sort(newArr, "acs");
                List<string> descArr = ListUtils.Sort(newArr, "desc");
                List<string> llArr = ListUtils.Concat(acsArr, descArr);
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
                string first = ListUtils.Shift(args);
                string last = ListUtils.Pop(args);
                int? length1 = ListUtils.Unshift(args, first);
                int? length2 = ListUtils.Push(args, last);
                int? length3 = length1 + length2;
                string longStr = "long" + first + last;
                string fullStr = string.Join(",", args);
                List<string> newArr = new List<string>
                {
                    "test"
                };
                List<string> cArr = ListUtils.Concat(newArr, args);
                List<string> acsArr = ListUtils.Sort(newArr, "acs");
                List<string> descArr = ListUtils.Sort(newArr, "desc");
                List<string> llArr = ListUtils.Concat(acsArr, descArr);
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
            Thread.Sleep(10);
            string ms = JSONUtils.SerializeObject(m);
            object ma = JsonConvert.DeserializeObject(ms);
            string arrStr = "[1,2,3,4]";
            object arr = JsonConvert.DeserializeObject(arrStr);
            object res = JSONUtils.readPath(m, "$.key4.key5");
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
            await Task.Delay(10);
            string ms = JSONUtils.SerializeObject(m);
            object ma = JsonConvert.DeserializeObject(ms);
            string arrStr = "[1,2,3,4]";
            object arr = JsonConvert.DeserializeObject(arrStr);
            object res = JSONUtils.readPath(m, "$.key4.key5");
        }

        public static object ReturnAny()
        {
            throw new NotImplementedException();
        }

        public static void Main(List<string> args)
        {
            int? a = (int?)(args[0]) + 10;
            string b = (string)a + args[1] + (string)ReturnAny();
            int? c = (int?)(b) + (int?)(a) + (int?)(ReturnAny());
            sbyte? d = (sbyte?)b + (sbyte?)a + (sbyte?)ReturnAny();
            short? e = (short?)b + (short?)a + (short?)ReturnAny();
            int? f = (int?)(b) + (int?)(a) + (int?)(ReturnAny());
            long? g = (long?)b + (long?)a + (long?)ReturnAny();
            long? h = (long?)b + (long?)a + (long?)ReturnAny();
            ulong? i = (ulong?)b + (ulong?)a + (ulong?)ReturnAny();
            byte? j = (byte?)b + (byte?)a + (byte?)ReturnAny();
            ushort? k = (ushort?)b + (ushort?)a + (ushort?)ReturnAny();
            uint? l = (uint?)b + (uint?)a + (uint?)ReturnAny();
            ulong? m = (ulong?)b + (ulong?)a + (ulong?)ReturnAny();
            float? n = (float?)b + (float?)a + (float?)ReturnAny();
            double? o = (double?)b + (double?)a + (double?)ReturnAny();
            if ((bool?)args[2])
            {
                // bytes 强转只允许传字符串
                byte[] data = (byte[])(b);
                int? length = data.Length;
                object test = data;
                Dictionary<string, string> maps = new Dictionary<string, string>
                {
                    {"key", "value"},
                };
                Dictionary<string, object> obj = Core.ToObject(maps);
                Stream ws = (Stream)obj;
                Stream rs = StreamUtils.BytesReadable(maps);
                data = StreamUtils.Read(rs, 30);
                if (!data.IsNull())
                {
                    ws.Write(data, 0, data.Length);
                }
            }
            string defaultVal = (string)Core.GetDefaultValue(args[0], args[1]);
            if (defaultVal == b)
            {
                return ;
            }
            // test binaryOp
            if ((string)(d > 0))
            {
            }
            if (!((bool?)args[2] || (bool?)args[0]).IsNull())
            {
            }
            if ((sbyte?)(c + d) > 0)
            {
            }
            if ((short?)(c + d) > 0)
            {
            }
            if ((int?)(c + d) > 0)
            {
            }
            if ((long?)(c + d) > 0)
            {
            }
            if ((byte?)(c + d) > 0)
            {
            }
            if ((ushort?)(c + d) > 0)
            {
            }
            if ((uint?)(c + d) > 0)
            {
            }
            if ((ulong?)(c + d) > 0)
            {
            }
            if ((long?)(c + d) > 0)
            {
            }
            if ((ulong?)(c + d) > 0)
            {
            }
            if ((float?)(n + n) > 0f)
            {
            }
            if ((double?)(o + o) > 0)
            {
            }
            if ((bool?)(c + d))
            {
            }
        }

        public static void BytesTest(List<string> args)
        {
            string fullStr = string.Join(",", args);
            byte[] data = StringUtils.ToBytes(fullStr, "utf8");
            string newFullStr = Encoding.UTF8.GetString(data);
            if (fullStr != newFullStr)
            {
                return ;
            }
            string hexStr = BytesUtils.ToHex(data);
            string base64Str = Convert.ToBase64String(data);
            int? length = data.Length;
            string obj = Encoding.UTF8.GetString(data);
            byte[] data2 = BytesUtils.From(base64Str, "base64");
        }

        public static async Task BytesTestAsync(List<string> args)
        {
            string fullStr = string.Join(",", args);
            byte[] data = StringUtils.ToBytes(fullStr, "utf8");
            string newFullStr = Encoding.UTF8.GetString(data);
            if (fullStr != newFullStr)
            {
                return ;
            }
            string hexStr = BytesUtils.ToHex(data);
            string base64Str = Convert.ToBase64String(data);
            int? length = data.Length;
            string obj = Encoding.UTF8.GetString(data);
            byte[] data2 = BytesUtils.From(base64Str, "base64");
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
            Dictionary<string, object> mapTest3 = ConverterUtils.Merge(mapTest , mapTest2);
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
            Dictionary<string, object> mapTest3 = ConverterUtils.Merge(mapTest , mapTest2);
            if (mapTest3.Get("key1") == "value4")
            {
                return ;
            }
        }

        public static void ModelTestCase(List<string> args)
        {
            Test test = new Test
            {
                Name = "test",
            };
            Dictionary<string, object> testMap = test.ToMap();
            int? len = testMap.Count;
        }

        public static async Task ModelTestCaseAsync(List<string> args)
        {
            Test test = new Test
            {
                Name = "test",
            };
            Dictionary<string, object> testMap = test.ToMap();
            int? len = testMap.Count;
        }

    }
}

