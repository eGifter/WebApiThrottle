namespace WebApiThrottle
{
    public enum ThrottlePolicyType : int
    {
        IpThrottling = 1,
        ClientThrottling,
        EndpointThrottling,
        MethodThrottling
    }
}
