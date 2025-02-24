namespace FUForum.BackendServer.Services;

public interface ISequenceService
{
    Task<int> GetKnowledgeBaseNewId();
}