using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MoldManager.WebUI.Models.GridRowModel;
using TechnikSys.MoldManager.Domain.Entity;
using TechnikSys.MoldManager.Domain.Abstract;

namespace MoldManager.WebUI.Models.GridViewModel
{
    public class POContentGridViewModel
    {
        public List<POContentGridRowModel> rows;
        public int Page;
        public int Total;
        public int Records;

        public POContentGridViewModel(List<POContent> POContents,
            List<PurchaseItem> _items,
            IPurchaseRequestRepository PRRepository, //IPurchaseItemRepository PurchaseItemRepository
            ITaskRepository TaskRepository)
        {
            rows = new List<POContentGridRowModel>();
            string _eta;
            string _prNumber;
            List<Task> _tasks = TaskRepository.Tasks.ToList();
            List<PurchaseRequest> _preqs = PRRepository.PurchaseRequests.ToList();

            foreach (POContent _poContent in POContents)
            {
                PurchaseItem _item = _items.Where(p => p.PurchaseItemID == _poContent.PurchaseItemID).FirstOrDefault() ?? new PurchaseItem();// (PurchaseItemRepository.QueryByID(_poContent.PurchaseItemID) ?? new PurchaseItem());
                Task _task = _tasks.Where(t => t.TaskID == _item.TaskID) .FirstOrDefault() ?? new Task();//(TaskRepository.QueryByTaskID(_item.TaskID) ?? new Task());
                double time;
                if (_task.TaskType == 1)
                {
                    time = Math.Round(_task.Time / (_task.R + _task.F) / 60, 2);
                }
                else //if (Task.TaskType == 4)
                {
                    time = Math.Round(_task.Time / (_task.Quantity) / 60, 2);
                }

                try
                {
                    PurchaseRequest _pr = _preqs.Where(p => p.PurchaseRequestID == _item.PurchaseRequestID).FirstOrDefault() ?? new PurchaseRequest();//PRRepository.GetByID(_item.PurchaseRequestID);
                    _eta = _pr.DueDate.ToString("yyyy-MM-dd");
                    _prNumber = _pr.PurchaseRequestNumber;
                }
                catch
                {
                    _eta = "";
                    _prNumber = "";
                }
                rows.Add(new POContentGridRowModel(_poContent, _item, _eta, _prNumber, time));
            }
            Page = 1;
            Total = POContents.Count();
            Records = 500;
        }
    }
}