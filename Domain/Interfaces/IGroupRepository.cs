

using Domain.Models;

namespace Domain.Interfaces;

public interface IGroupRepository
{
    void AddGroup(Group group);
    void DeleteGroup(int id);
    void UpdateGroup(Group group);
    List<Group> GetGroups();
    Group GetGroup(int id);
}
