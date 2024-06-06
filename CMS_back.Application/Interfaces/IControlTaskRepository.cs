using CMS_back.DTO;
using CMS_back.Models;

namespace CMS_back.Interfaces
{
    public interface IControlTaskRepository
    {
        Task<Control_Task> Create(Control_Task task, List<string> usersTasksIds, string controlId);
        Task<Control_Task> UpdateTask(Control_Task task, List<string> usersTasksIds);
        Task<IEnumerable<ControlTaskResultDTO>>? GetTasksOfControl(string controlId);
        Task<IEnumerable<ControlTaskResultDTO>>? GetUserTasks(string controlId, string userId);
        Task<Control_Task?> GetTaskByID(string taskId);
        Task<bool> DeleteTask(string taskId);
        Task<bool> FinishTask(string taskId);


    }
}