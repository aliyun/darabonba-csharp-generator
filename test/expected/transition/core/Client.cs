using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tea;
using Tea.Utils;
using Ecs20140526Client = AlibabaCloud.SDK.Ecs20140526.Client;
using AlibabaCloud.OpenApiClient.Models;
using AlibabaCloud.SDK.Ecs20140526.Models;
using AlibabaCloud.TeaUtil;

namespace AlibabaCloud.OpenApiClient
{
    public class Client 
    {


        /// <term><b>Description:</b></term>
        /// <description>
        /// <para>Initialization Initialize the Client with the AccessKey of the account</para>
        /// </description>
        public static async Task<Ecs20140526Client> InitializationAsync(string regionId)
        {
            // The project code leakage may result in the leakage of AccessKey, posing a threat to the security of all resources under the account. The following code examples are for reference only.
            // It is recommended to use the more secure STS credential. For more credentials, please refer to: https://www.alibabacloud.com/help/en/alibaba-cloud-sdk-262060/latest/credentials-settings-2.
            Config config = new Config
            {
                // Required, please ensure that the environment variables ALIBABA_CLOUD_ACCESS_KEY_ID is set.
                AccessKeyId = Environment.GetEnvironmentVariable("ALIBABA_CLOUD_ACCESS_KEY_ID"),
                // Required, please ensure that the environment variables ALIBABA_CLOUD_ACCESS_KEY_SECRET is set.
                AccessKeySecret = Environment.GetEnvironmentVariable("ALIBABA_CLOUD_ACCESS_KEY_SECRET"),
                RegionId = regionId,
            };
            return new Ecs20140526Client(config);
        }


        public static async Task Main(string[] args)
        {
            Ecs20140526Client client = await InitializationAsync("cn-hangzhou");
            Console.WriteLine("CreateSecurityGroup");
            CreateSecurityGroupResponseBody createSecurityGroupResponseBody = await CreateSecurityGroupAsync(client);
            if (createSecurityGroupResponseBody == null)
            {
                Console.WriteLine("Failed to complete the task \'CreateSecurityGroup\'.");
                return ;
            }
            string securityGroupId = createSecurityGroupResponseBody.SecurityGroupId;
            Console.WriteLine("AuthorizeSecurityGroup");
            AuthorizeSecurityGroupResponseBody authorizeSecurityGroupResponseBody = await AuthorizeSecurityGroupAsync(client, securityGroupId);
            if (authorizeSecurityGroupResponseBody == null)
            {
                Console.WriteLine("Failed to complete the task \'AuthorizeSecurityGroup\'.");
                return ;
            }
            Console.WriteLine("RunInstances");
            RunInstancesResponseBody runInstancesResponseBody = await CreateInstanceAsync(client, securityGroupId);
            if (runInstancesResponseBody == null)
            {
                Console.WriteLine("Failed to complete the task \'RunInstances\'.");
                return ;
            }
            List<string> instanceIds = runInstancesResponseBody.InstanceIdSets.InstanceIdSet;
            Console.WriteLine("DescribeInstances");
            bool? isWaitForInstancesRunningSuccess = (await WaitForInstancesRunningAsync(client, instanceIds)).Value;
            if (!isWaitForInstancesRunningSuccess.Value)
            {
                Console.WriteLine("Failed to complete the task \'DescribeInstances\' after 5 polling attempts.");
                return ;
            }
            Console.WriteLine("log instances info");
            DescribeInstancesResponseBody describeInstancesResponseBody = await LogInstancesInfoAsync(client, instanceIds);
            if (describeInstancesResponseBody == null)
            {
                Console.WriteLine("Failed to complete the task \'log instances info\'.");
                return ;
            }
        }


        /// <term><b>Description:</b></term>
        /// <description>
        /// <para>CreateSecurityGroup</para>
        /// </description>
        /// 
        /// <param name="client">
        /// Ecs20140526
        /// </param>
        /// 
        /// <returns>
        /// The response body of CreateSecurityGroup
        /// </returns>
        public static async Task<CreateSecurityGroupResponseBody> CreateSecurityGroupAsync(Ecs20140526Client client)
        {
            try
            {
                CreateSecurityGroupRequest request = new CreateSecurityGroupRequest
                {
                    RegionId = "cn-hangzhou",
                    // The name of the security group
                    SecurityGroupName = "MySecurityGroup",
                    // The description of the security group
                    Description = "sgDescription",
                };
                CreateSecurityGroupResponse response = await client.CreateSecurityGroupAsync(request);
                return response.Body;
            }
            catch (TeaException error)
            {
                Console.WriteLine(error.Message);
                return null;
            }
        }


        /// <term><b>Description:</b></term>
        /// <description>
        /// <para>AuthorizeSecurityGroup</para>
        /// </description>
        /// 
        /// <param name="client">
        /// Ecs20140526
        /// </param>
        /// <param name="securityGroupId">
        /// The ID of the security group
        /// </param>
        /// 
        /// <returns>
        /// The response body of AuthorizeSecurityGroup
        /// </returns>
        public static async Task<AuthorizeSecurityGroupResponseBody> AuthorizeSecurityGroupAsync(Ecs20140526Client client, string securityGroupId)
        {
            try
            {
                AuthorizeSecurityGroupRequest request = new AuthorizeSecurityGroupRequest
                {
                    RegionId = "cn-hangzhou",
                    // The security group rules
                    Permissions = new List<AuthorizeSecurityGroupRequest.AuthorizeSecurityGroupRequestPermissions>
                    {
                        new AuthorizeSecurityGroupRequest.AuthorizeSecurityGroupRequestPermissions
                        {
                            IpProtocol = "tcp",
                            PortRange = "22/22",
                            SourceCidrIp = "0.0.0.0/0",
                            Priority = "1",
                        },
                        new AuthorizeSecurityGroupRequest.AuthorizeSecurityGroupRequestPermissions
                        {
                            IpProtocol = "tcp",
                            PortRange = "22/22",
                            SourceCidrIp = "0.0.0.0/0",
                            Priority = "1",
                        }
                    },
                    // The ID of the security group
                    SecurityGroupId = securityGroupId,
                };
                AuthorizeSecurityGroupResponse response = await client.AuthorizeSecurityGroupAsync(request);
                return response.Body;
            }
            catch (TeaException error)
            {
                Console.WriteLine(error.Message);
                return null;
            }
        }


        /// <term><b>Description:</b></term>
        /// <description>
        /// <para>RunInstances</para>
        /// </description>
        /// 
        /// <param name="client">
        /// Ecs20140526
        /// </param>
        /// <param name="securityGroupId">
        /// The ID of the security group to which you want to assign the instance
        /// </param>
        /// 
        /// <returns>
        /// The response body of RunInstances
        /// </returns>
        public static async Task<RunInstancesResponseBody> CreateInstanceAsync(Ecs20140526Client client, string securityGroupId)
        {
            try
            {
                RunInstancesRequest request = new RunInstancesRequest
                {
                    RegionId = "cn-hangzhou",
                    // The ID of the image
                    ImageId = "ubuntu_18_04_64_20G_alibase_20210702.vhd",
                    // The instance type
                    InstanceType = "ecs.g5.large",
                    // Details about the image options
                    ImageOptions = new RunInstancesRequest.RunInstancesRequestImageOptions
                    {
                        LoginAsNonRoot = true,
                    },
                    // The desired number of ECS instances that you want to create
                    Amount = 1,
                    // The ID of the security group to which you want to assign the instance
                    SecurityGroupId = securityGroupId,
                };
                RunInstancesResponse response = await client.RunInstancesAsync(request);
                return response.Body;
            }
            catch (TeaException error)
            {
                Console.WriteLine(error.Message);
                return null;
            }
        }


        /// <term><b>Description:</b></term>
        /// <description>
        /// <para>DescribeInstances</para>
        /// </description>
        /// 
        /// <param name="client">
        /// Ecs20140526
        /// </param>
        /// <param name="instanceIds">
        /// The ID of the instance
        /// </param>
        /// 
        /// <returns>
        /// Whether the operation was successful
        /// </returns>
        public static async Task<bool?> WaitForInstancesRunningAsync(Ecs20140526Client client, List<string> instanceIds)
        {
            long? MAX_RETRY_TIMES = 5;
            long? retryCount = 0;

            while (retryCount < MAX_RETRY_TIMES) {
                try
                {
                    DescribeInstancesRequest request = new DescribeInstancesRequest
                    {
                        RegionId = "cn-hangzhou",
                        // The ID of the instance
                        //   instanceIds = $JSON.stringify(instanceIds),
                        InstanceIds = Common.ToJSONString(instanceIds),
                    };
                    DescribeInstancesResponse response = await client.DescribeInstancesAsync(request);
                    List<DescribeInstancesResponseBody.DescribeInstancesResponseBodyInstances.DescribeInstancesResponseBodyInstancesInstance> instances = response.Body.Instances.Instance;
                    if ((CheckInstancesRunning(instances)).Value)
                    {
                        return true;
                    }
                    await Task.Delay(3);
                    retryCount++;
                }
                catch (TeaException error)
                {
                    Console.WriteLine(error.Message);
                    return false;
                }
            }
            return false;
        }


        /// <term><b>Description:</b></term>
        /// <description>
        /// <para>log instances info</para>
        /// </description>
        /// 
        /// <param name="client">
        /// Ecs20140526
        /// </param>
        /// <param name="instanceIds">
        /// The ID of the instance
        /// </param>
        /// 
        /// <returns>
        /// The response body of DescribeInstances
        /// </returns>
        public static async Task<DescribeInstancesResponseBody> LogInstancesInfoAsync(Ecs20140526Client client, List<string> instanceIds)
        {
            try
            {
                DescribeInstancesRequest request = new DescribeInstancesRequest
                {
                    RegionId = "cn-hangzhou",
                    // The ID of the instance
                    //   instanceIds = $JSON.stringify(instanceIds),
                    InstanceIds = Common.ToJSONString(instanceIds),
                };
                DescribeInstancesResponse response = await client.DescribeInstancesAsync(request);
                List<DescribeInstancesResponseBody.DescribeInstancesResponseBodyInstances.DescribeInstancesResponseBodyInstancesInstance> instances = response.Body.Instances.Instance;

                foreach (var instance in instances) {
                    Console.WriteLine("Instance ID:" + instance.InstanceId);
                    Console.WriteLine("Status:" + instance.Status);
                    Console.WriteLine("CPU:" + instance.Cpu);
                    Console.WriteLine("Memory:" + instance.Memory + "MB");
                    Console.WriteLine("Instance Type:" + instance.InstanceType);
                    Console.WriteLine("OS:" + instance.OSType + "(" + instance.OSName + ")");
                }
                return response.Body;
            }
            catch (TeaException error)
            {
                Console.WriteLine(error.Message);
                return null;
            }
        }

        public static bool? CheckInstancesRunning(List<DescribeInstancesResponseBody.DescribeInstancesResponseBodyInstances.DescribeInstancesResponseBodyInstancesInstance> targets)
        {

            foreach (var target in targets) {
                string status = target.Status;
                if (status == "Running")
                {
                    Console.WriteLine("   test " + status);
                    return false;
                }
            }
            return true;
        }

    }
}

