using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Abstract
{
    public interface ICNCItemRepository
    {
        IQueryable<CNCItem> CNCItems { get; }

        int Save(CNCItem CNCItem);

        CNCItem QueryByID(int CNCItemID);

        string SetToPrint(int CNCItemID, bool Reprint = false); 

        void SetPrinted(int CNCItemID);

        CNCItem QueryByLabel(string Label);

        IEnumerable<CNCItem> QueryByTaskID(int TaskID);

        void SetPosition(string Label, string Position);

        void SetFinish(int CNCItemID);

        void SetReady(int CNCItemID);

        void SetRequired(int CNCItemID);

        void SetUnRequired(int CNCItemID);
    }
}
