namespace spgsWebTools.FunctionHelpers.Types
{
    public interface ISecretService
    {
        Task<string> GetClientSecretAsync(string secretName);
        Task<string> GetFlowEndpointAsync(string secretName);
    }
}