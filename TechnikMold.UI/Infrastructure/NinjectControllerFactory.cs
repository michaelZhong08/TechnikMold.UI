using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;
using TechnikSys.MoldManager.Domain.Abstract;
using TechnikSys.MoldManager.Domain.Concrete;


namespace TechnikSys.WebUI.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernel;

        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController)ninjectKernel.Get(controllerType);
        }

        private void AddBindings()
        {
            ninjectKernel.Bind<IBrandRepository>().To<BrandRepository>();
            ninjectKernel.Bind<IDepPhaseRepository>().To<DepPhaseRepository>();
            ninjectKernel.Bind<ICustomerRepository>().To<CustomerRepository>();
            ninjectKernel.Bind<IDepartmentRepository>().To<DepartmentRepository>();
            ninjectKernel.Bind<IListTypeRepository>().To<ListTypeRepository>();
            ninjectKernel.Bind<IListValueRepository>().To<ListValueRepository>();
            ninjectKernel.Bind<IMaterialRepository>().To<MaterialRepository>();
            ninjectKernel.Bind<IPartCodeRepository>().To<PartCodeRepository>();
            ninjectKernel.Bind<IPartRepository>().To<PartRepository>();
            ninjectKernel.Bind<IPartTypeRepository>().To<PartTypeRepository>();
            ninjectKernel.Bind<IPhaseModificationRepository>().To<PhaseModificationRepository>();
            ninjectKernel.Bind<IPhaseRepository>().To<PhaseRepository>();
            ninjectKernel.Bind<IPRContentRepository>().To<PRContentRepository>();
            ninjectKernel.Bind<IProjectPhaseRepository>().To<ProjectPhaseRepository>();
            ninjectKernel.Bind<IProjectRepository>().To<ProjectRepository>();
            ninjectKernel.Bind<IQRQuotationRepository>().To<QRQuotationRepository>();
            ninjectKernel.Bind<IQRSupplierRepository>().To<QRSupplierRepository>();
            ninjectKernel.Bind<IPurchaseRequestRepository>().To<PurchaseRequestRepository>();
            ninjectKernel.Bind<IRoleRepository>().To<RoleRepository>();
            ninjectKernel.Bind<IStockChangeRepository>().To<StockChangeRepository>();
            ninjectKernel.Bind<IStockPartRepository>().To<StockPartRepository>();
            ninjectKernel.Bind<ISupplierRepository>().To<SupplierRepository>();
            ninjectKernel.Bind<ITaskHourRepository>().To<TaskHourRepository>();
            ninjectKernel.Bind<ITaskRepository>().To<TaskRepository>();
            ninjectKernel.Bind<IUserRepository>().To<UserRepository>();
            ninjectKernel.Bind<ITaskHourCostRepository>().To<TaskHourCostRepository>();
            ninjectKernel.Bind<IProjectRoleRepository>().To<ProjectRoleRepository>();
            ninjectKernel.Bind<IHardnessRepository>().To<HardnessRepository>();
            ninjectKernel.Bind<ISequenceRepository>().To<SequenceRepository>();
            ninjectKernel.Bind<IPRProcessRepository>().To<PRProcessRepository>();
            ninjectKernel.Bind<IContactRepository>().To<ContactRepository>();
            ninjectKernel.Bind<IProcessRecordRepository>().To<ProcessRecordRepository>();
            ninjectKernel.Bind<ICostCenterRepository>().To<CostCenterRepository>();
            ninjectKernel.Bind<IWarehouseRepository>().To<WarehouseRepository>();
            ninjectKernel.Bind<IWarehouseStockRepository>().To<WarehouseStockRepository>();
            ninjectKernel.Bind<IPurchaseOrderRepository>().To<PurhaseOrderRepository>();
            ninjectKernel.Bind<IPOContentRepository>().To<POContentRepository>();
            ninjectKernel.Bind<ICAMTasksRepository>().To<CAMTaskRepository>();
            ninjectKernel.Bind<ICNCItemRepository>().To<CNCItemRepository>();
            ninjectKernel.Bind<IQCInfoRepository>().To<QCInfoRepository>();
            ninjectKernel.Bind<IPartImportRecordRepository>().To<PartImportRecordRepository>();
            ninjectKernel.Bind<IWarehouseRecordRepository>().To<WarehouseRecordRepository>();
            ninjectKernel.Bind<IPositionRepository>().To<PositionRepository>();
            ninjectKernel.Bind<IQuotationRequestRepository>().To<QuotationRequestRepository>();
            ninjectKernel.Bind<IQRContentRepository>().To<QRContentRepository>();
            ninjectKernel.Bind<ISteelDrawingCADPartRepository>().To<SteelDrawingCADPartRepository>();
            ninjectKernel.Bind<IEleItemRepository>().To<ELEItemRepository>();

            ninjectKernel.Bind<ICharmillRepository>().To<CharmillRepository>();
            ninjectKernel.Bind<IMachineRepository>().To<MachineRepository>();
            ninjectKernel.Bind<ICNCMachInfoRepository>().To<CNCMachInfoRepository>();

            ninjectKernel.Bind<ISteelProgramRepository>().To<SteelProgramRepository>();
            ninjectKernel.Bind<ISteelGroupProgramRepository>().To<SteelGroupProgramRepository>();

            ninjectKernel.Bind<IEDMDetailRepository>().To<EDMDetailRepository>();
            ninjectKernel.Bind<IEDMSettingRepository>().To<EDMSettingRepository>();
            ninjectKernel.Bind<IPartStockRepository>().To<PartStockRepository>();
            ninjectKernel.Bind<IQCPointProgramRepository>().To<QCPointPorgramRepository>();
            ninjectKernel.Bind<ISystemConfigRepository>().To<SystemConfigRepository>();
            ninjectKernel.Bind<IQCSteelPointRepository>().To<QCSteelPointRepository>();
            ninjectKernel.Bind<ISteelCAMDrawingRepository>().To<SteelCAMDrawingRepository>();
            ninjectKernel.Bind<ICAMDrawingRepository>().To<CAMDrawingRepository>();
            ninjectKernel.Bind<IQCTaskRepository>().To<QCTaskRepository>();

            ninjectKernel.Bind<IQCCmmReportRepository>().To<QCCmmReportRepository>();
            ninjectKernel.Bind<IQCCmmFileSettingRepository>().To<QCCmmFileSettingRepository>();
            ninjectKernel.Bind<IWarehouseRequestRepository>().To<WarehouseRequestRepository>();
            ninjectKernel.Bind<IWarehouseRequestItemRepository>().To<WarehouseRequestItemRepository>();
            ninjectKernel.Bind<IOutStockFormRepository>().To<OutStockFormRepository>();
            ninjectKernel.Bind<IOutStockItemRepository>().To<OutStockItemRepository>();
            ninjectKernel.Bind<IPurchaseItemRepository>().To<PurchaseItemRepository>();
            ninjectKernel.Bind<IEDMTaskRecordRepository>().To<EDMTaskRecordRepository>();

            ninjectKernel.Bind<IPurchaseTypeRepository>().To<PurchaseTypeRepository>();
            ninjectKernel.Bind<IProjectRecordRepository>().To<ProjectRecordRepository>();
            ninjectKernel.Bind<IStockTypeRepository>().To<StockTypeRepository>();
            ninjectKernel.Bind<IWarehousePositionRepository>().To<WarehousePositionRepository>();
            ninjectKernel.Bind<IUserRoleRepository>().To<UserRoleRepository>();

            ninjectKernel.Bind<IReturnRequestRepository>().To<ReturnRequestRepository>();
            ninjectKernel.Bind<IReturnItemRepository>().To<ReturnItemRepository>();
            ninjectKernel.Bind<ISupplierBrandRepository>().To<SupplierBrandRepository>();
            ninjectKernel.Bind<IPartListRepository>().To<PartListRepository>();
            ninjectKernel.Bind<IQuotationFileRepository>().To<QuotationFileRepository>();

            ninjectKernel.Bind<IMGSettingRepository>().To<MGSettingRepository>();
            ninjectKernel.Bind<IWEDMSettingRepository>().To<WEDMSettingRepository>();
            ninjectKernel.Bind<IMachinesInfoRepository>().To<MachinesInfoRepository>();
            ninjectKernel.Bind<IWH_TaskPeriodTypeRepository>().To<WH_TaskPeriodTypeRepository>();
            ninjectKernel.Bind<IWH_WorkTypeRepository>().To<WH_WorkTypeRepository>();
            ninjectKernel.Bind<IWH_TaskPeriodRecordRepository>().To<WH_TaskPeriodRecordRepository>();
        }
    }
}