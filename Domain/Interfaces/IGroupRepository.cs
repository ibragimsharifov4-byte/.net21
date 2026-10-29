

using Domain.Models;

namespace Domain.Interfaces;

public interface IGroupRepository
{
    Task AddGroup(Group group);
    Task DeleteGroup(int id);
    Task UpdateGroup(Group group);
    Task<List<Group>> GetGroups();
    Task<Group?> GetGroup(int id);
}
