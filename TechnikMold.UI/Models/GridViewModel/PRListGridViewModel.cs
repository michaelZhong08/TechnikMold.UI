using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnikSys.MoldManager.Domain.Entity;
using TechnikSys.MoldManager.Domain.Abstract;
using MoldManager.WebUI.Models.GridRowModel;
using MoldManager.WebUI.Models.Helpers;

namespace MoldManager.WebUI.Models.GridViewModel
{
    public class PRListGridViewModel
    {
        public List<PRListGridRowModel> rows;
        public int Page;
        public int Total;
        public int Records;

        public PRListGridViewModel(IEnumerable<PurchaseRequest> Requests, 
            IUserRepository  UsersRepo, 
            PurchaseRequestStatus Status, 
            IProjectRepository ProjectRepository, 
            IPurchaseTypeRepository PurchaseTypeRepository, 
            IDepartmentRepository DeptRepository)
        {
            rows = new List<PRListGridRowModel>();
            string _userName, _reviewUser, _submitUser;
            string _dept;
            foreach (PurchaseRequest _request in Requests)
            {
                //Project _Project = ProjectRepository.Projects.Where(p => p.ProjectID == _request.ProjectID).FirstOrDefault();
                //_moldno = _Project.MoldNumber;
                try
                {
                    
                    _userName = UsersRepo.GetUserByID(_request.UserID).FullName;

                }
                catch
                {
                    _userName = "";
                }

                try
                {

                    _reviewUser = UsersRepo.GetUserByID(_request.ReviewUserID).FullName;

                }
                catch
                {
                    _reviewUser = "";
                }

                try
                {

                    _submitUser = UsersRepo.GetUserByID(_request.SubmitUserID).FullName;

                }
                catch
                {
                    _submitUser = "";
                }


                string _type = "";
                try
                {
                    _type=PurchaseTypeRepository.QueryByID(_request.PurchaseType).Name;
                }
                catch
                {
                    _type = "";
                }
                try
                {
                    _dept = DeptRepository.GetByID(_request.DepartmentID).Name;
                }
                catch
                {
                    _dept = "";
                }

                rows.Add(new PRListGridRowModel(_request, _userName, _type, _dept, _submitUser, _reviewUser));
            }
            Page = 1;
            Total = 100;
            Records = 30;
        }
    }
}