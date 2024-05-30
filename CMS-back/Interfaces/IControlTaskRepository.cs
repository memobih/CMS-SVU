using CMS_back.Models;

namespace CMS_back.Interfaces
{
    public interface IControlTaskRepository
    {
        Task<Control_Task> Create(Control_Task task,List<string> usersTasksIds, string controlId);
        Task<ICollection<Control_Task>?> GetTasksOfControl(string controlId);
        Task<Control_Task> UpdateTask(Control_Task task, List<string> usersTasksIds);
        Task<bool> DeleteTask(string taskId);
        Task<Control_Task> FinishTask(string taskId);
        Task<Control_Task?> GetTaskByID(string taskId);
        Task<ICollection<Control_Task>>? GetUserTasks(string controlId, string userId);
    }
}
