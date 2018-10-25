namespace WebApiThrottle
{
    public enum ThrottlePolicyType
    {
        IpThrottling = 1,
        ClientThrottling,
        EndpointThrottling,
        MethodThrottling
    }
}
