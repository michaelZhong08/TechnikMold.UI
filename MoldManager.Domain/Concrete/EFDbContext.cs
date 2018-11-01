/*
 * Create By:lechun1
 * 
 * Description: Data access provider for all concrete repositories
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using TechnikSys.MoldManager.Domain.Abstract;
using TechnikSys.MoldManager.Domain.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace TechnikSys.MoldManager.Domain.Concrete
{
    public class EFDbContext:DbContext
    {
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Base_DepPhase> Base_DepPhases { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<ListType> ListTypes { get; set; }
        public DbSet<ListValue> ListValues { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<PartCode> PartCodes { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<PartType> PartTypes { get; set; }
        public DbSet<PhaseModification> PhaseModifications { get; set; }
        public DbSet<Phase> Phases { get; set; }
        public DbSet<PRContent> PRContents { get; set; }
        public DbSet<ProjectPhase> ProjectPhases { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<QRQuotation> QRQuotations { get; set; }
        public DbSet<QRSupplier> QRSuppliers { get; set; }
        public DbSet<PurchaseRequest> PurchaseRequests { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<StockChange> StockChanges { get; set; }
        public DbSet<StockPart> StockParts { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<TaskHour> TaskHours { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<TaskHourCost> TaskHourCosts { get; set; }
        public DbSet<ProjectRole> ProjectRoles { get; set; }
        public DbSet<Hardness> Hardnesses { get; set; }
        public DbSet<Sequence> Sequences { get; set; }
        public DbSet<PRProcess> PRProcesses { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<SupplierCategory> SupplierCategories { get; set; }
        public DbSet<ProcessRecord> ProcessRecords { get; set; }
        public DbSet<CostCenter> CostCenters { get; set; }
        public DbSet<WarehouseStock> WarehouseStocks { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<POContent> POContents { get; set; }
        public DbSet<WarehouseRecord> WarehouseRecords { get; set; }
        public DbSet<CAMTask> CAMTasks { get; set; }
        public DbSet<CNCItem> CNCItems { get;set;}
        //public DbSet<EDMItem> EDMItems { get; set; }
        public DbSet<QCInfo> QCInfoes { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<QuotationRequest> QuotationRequests{ get; set; }
        public DbSet<PartImportRecord> PartImportRecords { get; set; }
        public DbSet<QRContent> QRContents { get; set; }

        //public DbSet<SteelDrawingCADPart> SteelDrawingCADPart { get; set; }
        public DbSet<Charmill> Charmills { get; set; }
        public DbSet<EleItem> EleItems { get; set; }

        public DbSet<Machine> Machines { get; set; }
        public DbSet<CNCMachInfo> CNCMachInfoes { get; set; }

        public DbSet<SteelGroupProgram> SteelGroupPrograms { get; set; }
        public DbSet<SteelProgram> SteelPrograms { get; set; }

        public DbSet<EDMDetail> EDMDetails { get; set; }
        public DbSet<EDMSetting> EDMSettings { get; set; }
        public DbSet<PartStock> PartStocks { get; set; }
        public DbSet<QCPointProgram> QCPointPrograms { get; set; }

        public DbSet<SystemConfig> SystemConfigs { get; set; }
        public DbSet<QCSteelPoint> QCSteelPoints { get; set; }
        public DbSet<SteelCAMDrawing> SteelCAMDrawngs { get; set; }
        public DbSet<SteelDrawingCADPart> SteelDrawingCADParts { get; set; }
        public DbSet<CAMDrawing> CAMDrawings { get; set; }
        public DbSet<QCTask> QCTasks { get; set; }
        public DbSet<QCCmmReport> QCCmmReports { get; set; }
        public DbSet<QCCmmFileSetting> QCCmmFileSettings { get; set; }

        public DbSet<WarehouseRequest> WarehouseRequests { get; set; }
        public DbSet<WarehouseRequestItem> WarehouseRequestItems { get; set; }
        public DbSet<OutStockForm> OutStockForms { get; set; }
        public DbSet<OutStockItem> OutStockItems { get; set; }
        public DbSet<WarehousePosition> WarehousePositions { get; set; }
        public DbSet<PurchaseItem> PurchaseItems { get; set; }

        public DbSet<EDMTaskRecord> EDMTaskRecords { get; set; }
        public DbSet<PurchaseType> PurchaseTypes { get; set; }
        public DbSet<ProjectRecord> ProjectRecords { get; set; }
        public DbSet<StockType> StockTypes { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }

        public DbSet<ReturnRequest> ReturnRequests { get; set; }

        public DbSet<ReturnItem> ReturnItems { get; set; }
        public DbSet<SupplierBrand> SupplierBrands { get; set; }

        public DbSet<PartList> PartLists { get; set; }
        public DbSet<QuotationFile> QuotationFiles { get; set; }
        public DbSet<MGTypeName> MGTypeNames { get; set; }
        public DbSet<MGSetting> MGSettings { get; set; }
        public DbSet<WEDMSetting> WEDMSettings { get; set; }
        public DbSet<WEDMCutSpeed> WEDMCutSpeeds { get; set; }
        public DbSet<WEDMPrecision> WEDMPrecisions { get; set; }
        public DbSet<MachinesInfo> MachinesInfo { get; set; }
        public DbSet<WH_TaskPeriodType> WH_TaskPeriodType { get; set; }
        public DbSet<WH_WorkType> WH_WorkType { get; set; }
        public DbSet<WH_TaskPeriodRecord> WH_TaskPeriodRecords { get; set; }
        public DbSet<SupplierGroup> SupplierGroups { get; set; }
        public DbSet<TaskType> TaskTypes { get; set; }
        public DbSet<PhaseTaskType> PhaseTaskTypes { get; set; }
    }
}
