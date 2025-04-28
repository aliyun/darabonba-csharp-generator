using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Darabonba;
using Darabonba.Utils;
using Ecs20140526Client = AlibabaCloud.SDK.Ecs20140526.Client;
using AlibabaCloud.OpenApiClient.Models;
using AlibabaCloud.SDK.Ecs20140526.Models;
using Darabonba.Exceptions;

namespace AlibabaCloud.OpenApiClient
{
    public class Client 
    {


        /// <term><b>Description:</b></term>
        /// <description>
        /// <para>Initialization  初始化公共请求参数</para>
        /// </description>
        public static async Task<Ecs20140526Client> InitializationAsync(string regionId)
        {
            // 工程代码泄露可能会导致 AccessKey 泄露，并威胁账号下所有资源的安全性。以下代码示例仅供参考。
            // 建议使用更安全的 STS 方式，更多鉴权访问方式请参见：https://help.aliyun.com/document_detail/378657.html。
            Config config = new Config
            {
                // 必填，请确保代码运行环境设置了环境变量 ALIBABA_CLOUD_ACCESS_KEY_ID
                AccessKeyId = Environment.GetEnvironmentVariable("ALIBABA_CLOUD_ACCESS_KEY_ID"),
                // 必填，请确保代码运行环境设置了环境变量 ALIBABA_CLOUD_ACCESS_KEY_SECRET
                AccessKeySecret = Environment.GetEnvironmentVariable("ALIBABA_CLOUD_ACCESS_KEY_SECRET"),
                // 您的可用区ID
                RegionId = regionId,
            };
            return new Ecs20140526Client(config);
        }


        public static async Task Main(string[] args)
        {
            Ecs20140526Client client = await InitializationAsync("cn-hangzhou");
            // 创建数据盘
            Log("Step1: 创建数据盘");
            CreateDiskResponseBody createDiskResponseBody = await CreateDiskAsync(client);
            if (createDiskResponseBody == null)
            {
                Log("任务'创建数据盘'失败。");
                return ;
            }
            string diskId = createDiskResponseBody.DiskId;
            Log("任务'创建数据盘'执行完成!");
            // 等待云盘创建完成
            Log("Step2: 等待云盘创建完成");
            bool? isWaitForDiskAvailableSuccess = await WaitForDiskAvailableAsync(client, diskId);
            if (!isWaitForDiskAvailableSuccess.Value)
            {
                Log("任务'等待云盘创建完成'失败。");
                return ;
            }
            Log("任务'等待云盘创建完成'执行完成!");
            Log("任务\"等待\"云盘\"建\"完\"成\"执行完成!");
            Log("\"\"");
            // 为实例挂载磁盘
            Log("Step3: 为实例挂载磁盘");
            AttachDiskResponseBody attachDiskResponseBody = await AttachDiskAsync(client, diskId);
            if (attachDiskResponseBody == null)
            {
                Log("任务'为实例挂载磁盘'失败。");
                return ;
            }
            Log("任务'为实例挂载磁盘'执行完成!");
            // 等待云盘挂载完成
            Log("Step4: 等待云盘挂载完成");
            if (!(await WaitForDiskAttachedAsync(client, diskId)).Value)
            {
                Log("任务'等待云盘挂载完成'失败。");
                return ;
            }
            if ((await WaitForDiskAttachedAsync(client, diskId)).Value)
            {
                Log("任务'等待云盘挂载完成'失败。");
                return ;
            }
            bool? isWaitForDiskAttachedSuccess = await WaitForDiskAttachedAsync(client, diskId);
            if (!isWaitForDiskAttachedSuccess.Value)
            {
                Log("任务'等待云盘挂载完成'失败。");
                return ;
            }
            if (isWaitForDiskAttachedSuccess.Value)
            {
                Log("任务'等待云盘挂载完成'失败。");
                return ;
            }
            Log("任务'等待云盘挂载完成'执行完成!");
            // 打印云盘信息
            Log("Step5: 打印云盘信息");
            DescribeDisksResponseBody describeDisksResponseBody = await LogDiskInfoAsync(client, diskId);
            if (describeDisksResponseBody == null)
            {
                Log("任务'打印云盘信息'失败。");
                return ;
            }
            Log("任务'打印云盘信息'执行完成!");
        }


        /// <term><b>Description:</b></term>
        /// <description>
        /// <para>创建数据盘</para>
        /// </description>
        /// 
        /// <param name="client">
        /// Ecs20140526
        /// </param>
        /// 
        /// <returns>
        /// The response body of CreateDisk
        /// </returns>
        public static async Task<CreateDiskResponseBody> CreateDiskAsync(Ecs20140526Client client)
        {
            try
            {
                CreateDiskRequest request = new CreateDiskRequest
                {
                    // 所属的地域ID
                    RegionId = "cn-hangzhou",
                    // 在指定可用区内创建一块按量付费磁盘
                    ZoneId = "cn-hangzhou-h",
                    // 磁盘名称
                    DiskName = "java-test",
                    // 磁盘描述
                    Description = "",
                    // 容量大小
                    Size = 40,
                    // 数据盘的磁盘种类
                    DiskCategory = "cloud_auto",
                    // 是否开启Burst（性能突发）
                    BurstingEnabled = true,
                };
                CreateDiskResponse response = await client.CreateDiskAsync(request);
                return response.Body;
            }
            catch (DaraException error)
            {
                Log("执行失败。错误信息：");
                Log(error.Message);
                return null;
            }
        }


        /// <term><b>Description:</b></term>
        /// <description>
        /// <para>等待云盘创建完成</para>
        /// </description>
        /// 
        /// <param name="client">
        /// Ecs20140526
        /// </param>
        /// <param name="diskId">
        /// 云盘、本地盘或弹性临时盘ID
        /// </param>
        /// 
        /// <returns>
        /// Whether the operation was successful
        /// </returns>
        public static async Task<bool?> WaitForDiskAvailableAsync(Ecs20140526Client client, string diskId)
        {
            // maximum retry attempts
            int? maxRetryTimes = 5;
            // current retry attempt
            int? retryIndex = 0;

            while (retryIndex < maxRetryTimes) {
                try
                {
                    DescribeDisksRequest request = new DescribeDisksRequest
                    {
                        RegionId = "cn-hangzhou",
                        // 云盘、本地盘或弹性临时盘ID
                        DiskIds = JSONUtils.SerializeObject(new List<string>
                        {
                            diskId
                        }),
                    };
                    DescribeDisksResponse response = await client.DescribeDisksAsync(request);
                    DescribeDisksResponseBody.DescribeDisksResponseBodyDisks.DescribeDisksResponseBodyDisksDisk disk = response.Body.Disks.Disk[0];
                    string diskStatus = disk.Status;
                    if (diskStatus == "Available")
                    {
                        return true;
                    }
                    await Task.Delay(3000);
                    retryIndex++;
                }
                catch (DaraException error)
                {
                    Log("执行失败。错误信息：");
                    Log(error.Message);
                    return false;
                }
            }
            return false;
        }


        /// <term><b>Description:</b></term>
        /// <description>
        /// <para>为实例挂载磁盘</para>
        /// </description>
        /// 
        /// <param name="client">
        /// Ecs20140526
        /// </param>
        /// <param name="diskId">
        /// 待挂载的磁盘ID
        /// </param>
        /// 
        /// <returns>
        /// The response body of AttachDisk
        /// </returns>
        public static async Task<AttachDiskResponseBody> AttachDiskAsync(Ecs20140526Client client, string diskId)
        {
            try
            {
                AttachDiskRequest request = new AttachDiskRequest
                {
                    // 待挂载ECS实例的ID
                    InstanceId = "i-bp159d628gxsw4gks8dd",
                    // 释放实例时
                    DeleteWithInstance = true,
                    // 待挂载的磁盘ID
                    DiskId = diskId,
                };
                AttachDiskResponse response = await client.AttachDiskAsync(request);
                return response.Body;
            }
            catch (DaraException error)
            {
                Log("执行失败。错误信息：");
                Log(error.Message);
                return null;
            }
        }


        /// <term><b>Description:</b></term>
        /// <description>
        /// <para>等待云盘挂载完成</para>
        /// </description>
        /// 
        /// <param name="client">
        /// Ecs20140526
        /// </param>
        /// <param name="diskId">
        /// 云盘、本地盘或弹性临时盘ID
        /// </param>
        /// 
        /// <returns>
        /// Whether the operation was successful
        /// </returns>
        public static async Task<bool?> WaitForDiskAttachedAsync(Ecs20140526Client client, string diskId)
        {
            // maximum retry attempts
            int? maxRetryTimes = 5;
            // current retry attempt
            int? retryIndex = 0;

            while (retryIndex < maxRetryTimes) {
                try
                {
                    DescribeDisksRequest request = new DescribeDisksRequest
                    {
                        RegionId = "cn-hangzhou",
                        // 云盘、本地盘或弹性临时盘ID
                        DiskIds = JSONUtils.SerializeObject(new List<string>
                        {
                            diskId
                        }),
                    };
                    DescribeDisksResponse response = await client.DescribeDisksAsync(request);
                    DescribeDisksResponseBody.DescribeDisksResponseBodyDisks.DescribeDisksResponseBodyDisksDisk disk = response.Body.Disks.Disk[0];
                    string diskStatus = disk.Status;
                    if (diskStatus == "In_use")
                    {
                        return true;
                    }
                    await Task.Delay(3000);
                    retryIndex++;
                }
                catch (DaraException error)
                {
                    Log("执行失败。错误信息：");
                    Log(error.Message);
                    return false;
                }
            }
            return false;
        }


        /// <term><b>Description:</b></term>
        /// <description>
        /// <para>打印云盘信息</para>
        /// </description>
        /// 
        /// <param name="client">
        /// Ecs20140526
        /// </param>
        /// <param name="diskId">
        /// 云盘、本地盘或弹性临时盘ID
        /// </param>
        /// 
        /// <returns>
        /// The response body of DescribeDisks
        /// </returns>
        public static async Task<DescribeDisksResponseBody> LogDiskInfoAsync(Ecs20140526Client client, string diskId)
        {
            try
            {
                DescribeDisksRequest request = new DescribeDisksRequest
                {
                    RegionId = "cn-hangzhou",
                    // 云盘、本地盘或弹性临时盘ID
                    DiskIds = JSONUtils.SerializeObject(new List<string>
                    {
                        diskId
                    }),
                };
                DescribeDisksResponse response = await client.DescribeDisksAsync(request);
                DescribeDisksResponseBody.DescribeDisksResponseBodyDisks.DescribeDisksResponseBodyDisksDisk disk = response.Body.Disks.Disk[0];
                Log("云盘ID: " + disk.DiskId + "信息");
                Log("diskCategory: " + disk.Category);
                Log("名称: " + disk.DiskName);
                Log("状态: " + disk.Status);
                Log("可用区: " + disk.ZoneId);
                Log("大小: " + disk.Size + "GiB");
                Log("挂载在实例 " + disk.InstanceId);
                return response.Body;
            }
            catch (DaraException error)
            {
                Log("执行失败。错误信息：");
                Log(error.Message);
                return null;
            }
        }

        // 自定义日志打印方法
        public static void Log(string message)
        {
            Console.WriteLine(message);
        }

        public static void TestMultiLines()
        {
            string certificate = "-----BEGIN CERTIFICATE-----\nMIIDlTCCAn2gAwIBAgIRALq6ehQjtveDrqiUoDu8088wDQYJKoZIhvcNAQELBQAw\ngYsxCzAJBgNVBAYTAkNOMRYwFAYDVQQKEw1BbGliYWJhIENsb3VkMTcwNQYDVQQL\nEy5BbGliYWJhIENsb3VkIENsaWVudCBTU0wgQ2VydGlmaWNhdGUgQXV0aG9yaXR5\nMSswKQYDVQQDEyJBbGl5dW4gRVNBIFJTQSBDQSAxMTExMDM0NDA0NjE4NjQ2MB4X\nDTI1MDMxMzExMDY1NFoXDTI2MDMxMzExMDY1NFowLjELMAkGA1UEBhMCQ04xHzAd\nBgNVBAMTFkVTQSBDbGllbnQgQ2VydGlmaWNhdGUwggEiMA0GCSqGSIb3DQEBAQUA\nA4IBDwAwggEKAoIBAQC5NK/ZpIHntyeWArDMHUG8biRBtj5MoVKvIuNANcaTDN9b\nmnEpLpc9Jw9TdTryY95h4U43C+SQ0mYiqAvgWQUSO3SGIPXLjVEEQKUz0+l5Sqpn\nGv+dc+/sDutRW5Qj0VfvcFtqniwCKNtmhLPUr5jtArIcpz2+bgWqTTF7biUHtFH1\nygRSgADpvC8OWv4CJL97g9TEn2bBcem/7L+bhDWZTA4ljmfsNF5S66d5kpD2YyoR\n6Ll7pWspVl0BfenDizK/bG166HhYTjf1lLKtdEaotZZBbx4UGO67LCpBgv4oMX/Q\n4ttio3oznmKKjmY44gBGYHGbAFKByYlOjSOcvj+DAgMBAAGjUDBOMB0GA1UdJQQW\nMBQGCCsGAQUFBwMBBggrBgEFBQcDAjAMBgNVHRMBAf8EAjAAMB8GA1UdIwQYMBaA\nFK8eYb/M6Hp3ZcGmoRJ3TLJuvyKdMA0GCSqGSIb3DQEBCwUAA4IBAQB5pOJ4DAm3\nQbKE4cdw4LSknu29XoOQSB4KcMfD7zdeBw5dAKtQlP1cWNXX0rlmPBRxFo6qRXGW\n+uu/mOj0waU+7O/LbLGTOHG/APEN/1pqs5g3jFjS0zqtby3FXvnhVjBOV5skbq2q\nOpT2u8gCgvssx2bCue4mJ9uO2JNO/Nel4yneBevpvxKTKUnX5akXDIY612RPQjtJ\nX74jlLepOgIn/PYeKiwQFs0YoGXAjaUp7wFlIAfW+ufqPOc2nMUnBsw7Dtr6IpEX\n9RZLKzcYBpP4ieP8Sl07CUKoM94cC75rwLI/RVmatly6SlBzx2G8qh7T3MDYAeT4\nSAVHJpmH3me9\n-----END CERTIFICATE-----\n";
        }

    }
}

