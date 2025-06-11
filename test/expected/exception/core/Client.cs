// This file is auto-generated, don't edit it. Thanks.

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Darabonba.Utils;
using Darabonba.Test.Exceptions;
using Darabonba.import.Exceptions;

namespace Darabonba.Test
{
    public class Client 
    {

        public static void ExceptionTest(List<string> args)
        {
            if (args.Count < 0)
            {
                throw new ExtendFileException
                {
                    Code = "name",
                    Key2 = "value2",
                    Data = new Dictionary<string, string>
                    {
                        {"name", "name"},
                        {"path", "100"},
                    },
                };
            }
        }

        public static async Task ExceptionTestAsync(List<string> args)
        {
            if (args.Count < 0)
            {
                throw new ExtendFileException
                {
                    Code = "name",
                    Key2 = "value2",
                    Data = new Dictionary<string, string>
                    {
                        {"name", "name"},
                        {"path", "100"},
                    },
                };
            }
        }

        public static void MultiTryCatch(int? a)
        {
            try
            {
                if (a > 0)
                {
                    throw new Err1Exception
                    {
                        Name = "str",
                        Code = "str",
                        Data = new Dictionary<string, string>
                        {
                            {"key1", "str"},
                        },
                    };
                }
                else if (a == 0)
                {
                    throw new Err2Exception
                    {
                        Name = "str",
                        Code = "str",
                        AccessErrMessage = "str2",
                    };
                }
                else if (a == -10)
                {
                    throw new Err3Exception
                    {
                        Name = "str",
                        Code = "str",
                    };
                }
                else
                {
                    throw new Darabonba.Exceptions.DaraException
                    {
                        Name = "str",
                        Code = "str",
                    };
                }
            }
            catch (Err1Exception err)
            {
                Console.WriteLine(err.Name);
            }
            catch (Err2Exception err)
            {
                Console.WriteLine(err.Name);
            }
            catch (Err3Exception err)
            {
                Console.WriteLine(err.Name);
            }
            catch (Darabonba.Exceptions.DaraException err)
            {
                Console.WriteLine(err.Name);
            }
            finally
            {
                string final = "ok";
            }
        }

        public static async Task MultiTryCatchAsync(int? a)
        {
            try
            {
                if (a > 0)
                {
                    throw new Err1Exception
                    {
                        Name = "str",
                        Code = "str",
                        Data = new Dictionary<string, string>
                        {
                            {"key1", "str"},
                        },
                    };
                }
                else if (a == 0)
                {
                    throw new Err2Exception
                    {
                        Name = "str",
                        Code = "str",
                        AccessErrMessage = "str2",
                    };
                }
                else if (a == -10)
                {
                    throw new Err3Exception
                    {
                        Name = "str",
                        Code = "str",
                    };
                }
                else
                {
                    throw new Darabonba.Exceptions.DaraException
                    {
                        Name = "str",
                        Code = "str",
                    };
                }
            }
            catch (Err1Exception err)
            {
                Console.WriteLine(err.Name);
            }
            catch (Err2Exception err)
            {
                Console.WriteLine(err.Name);
            }
            catch (Err3Exception err)
            {
                Console.WriteLine(err.Name);
            }
            catch (Darabonba.Exceptions.DaraException err)
            {
                Console.WriteLine(err.Name);
            }
            finally
            {
                string final = "ok";
            }
        }

    }
}

