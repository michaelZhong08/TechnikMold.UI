
//用户列表
function UserGrid(Keyword) {

    $("#UserGrid").jqGrid({
        url: '/User/Users?Keyword=' + Keyword,
        mtype: "GET",
        styleUI: 'Bootstrap',
        datatype: "json",
        colModel: [
            { lable: '', name: 'ID', hidden: true },
            { lable: "DepartmentID", name: "DepartmentID", hidden: true },
            { lable: '用户代码', name: '用户代码', width: 100 },
            { label: '域用户名', name: 'LogonName', key: true, width: 100 },
            { label: '姓名', name: 'FullName', width: 100 },
            { label: '部门', name: 'Department', width: 75 },
            { label: '岗位', name: 'Position', width: 75 },
            { label: '电话号码', name: 'Extension', width: 75 },
            { label: '手机号码', name: 'Mobile', width: 150 },
            { label: '电子邮件', name: 'Email', width: 150 },
        ],
        viewrecords: true,
        height: document.documentElement.clientHeight - 250,
        width: document.body.clientWidth * 0.875,
        rowNum: 500,
        multiselect: true,
        sortname: 'LogonName',
        sortorder: 'asc',
        loadonce: true,
        ondblClickRow: function () {
            console.log(event.target);
            var _row = $(event.target).closest("tr.jqgrow")[0].rowIndex;
            var _id = $("#UserGrid").getCell(_row, "ID");
            console.log($("#UserGrid").getCell(_row, "FullName"))
            LoadUser(_id);
        },
    });
}

//项目列表
function ProjectGrid(keyword, state, type, depID) {
    if (state == undefined) {
        state = 1;
    }
    if (type == undefined) {
        type = 1;
    }
    if (keyword == undefined) {
        keyword = "";
    }
    if (depID == undefined) {
        depID = 18;
    }
    var _url = '/Project/JsonProjects?keyword=' + keyword + "&State=" + state + "&Type=" + type + "&DepID=" + depID;
    $("#ProjectGrid").jqGrid({
        url: _url,
        mtype: "GET",
        styleUI: 'Bootstrap',
        datatype: "json",
        colModel: [
            { label: '', name: 'ID', width: 75, hidden: true },
            {
                label: '项目号', name: 'ProjectNo', key: true, width: 75, align: "center",
                cellattr: function (rowID, tv, rawobject, cm, rdata) {
                    return 'id=\'ProjectNo' + rowID + "\'";
                }, hidden: true, sorttable: false
            },
            {
                label: '模具号', name: 'MoldNo', width: 75,
                cellattr: function (rowID, tv, rawobject, cm, rdata) {
                    return 'id=\'MoldNo' + rowID + "\'";
                }, sorttable: false
            },
            {
                label: "类型", name: 'Type', width: 60, sorttable: false
            },
            {
                label: 'CAD', name: 'CAD', width: 75, sorttable: false,
                cellattr: function () { return 'id=1'; }
            },
            {
                label: 'CAM', name: 'CAM', width: 75, sorttable: false,
                cellattr: function () { return 'id=2'; }
            },
            {
                label: '采购', name: 'Purchase', width: 75, sorttable: false,
                cellattr: function () { return 'id=3'; }
            },
            {
                label: '开粗', name: 'Prototype', width: 75, sorttable: false,
                cellattr: function () { return 'id=4'; }
            },
            {
                label: 'CNC开粗', name: 'CNCPrototype', width: 75, sorttable: false,
                cellattr: function () { return 'id=5'; }
            },
            {
                label: '热处理', name: 'HeatProcess', width: 75, sorttable: false,
                cellattr: function () { return 'id=6'; }
            },
            {
                label: '磨床', name: 'Grinding', width: 75, sorttable: false,
                cellattr: function () { return 'id=7'; }
            },
            {
                label: 'CNC', name: 'CNC', width: 75, sorttable: false,
                cellattr: function () { return 'id=8'; }
            },
            {
                label: 'EDM', name: 'EDM', width: 75, sorttable: false,
                cellattr: function () { return 'id=9'; }
            },
            {
                label: 'WEDM', name: 'WEDM', width: 75, sorttable: false,
                cellattr: function () { return 'id=10'; }
            },
            {
                label: '装配', name: 'Assembly', width: 75, sorttable: false,
                cellattr: function () { return 'id=11'; }
            },
            {
                label: '试模', name: 'FOT', width: 75, sorttable: false,
                cellattr: function () { return 'id=12'; }
            },
            {
                label: 'OTS', name: 'OTS', width: 75, sorttable: false,
                cellattr: function () { return 'id=13'; }
            },
            {
                label: 'PPAP', name: 'PPAP', width: 75, sorttable: false,
                cellattr: function () { return 'id=14'; }
            },
            {
                label: '备注', name: 'Memo', width: 100, sorttable: false,
                cellattr: function (rowID, tv, rawobject, cm, rdata) {
                    return 'id=\'Memo' + rowID + "\'";
                }
            }
        ],
        viewrecords: true,
        height: document.documentElement.clientHeight - 190,
        width: document.body.clientWidth * 0.98,
        rownumbers: true, // show row numbers
        rownumWidth: 25, // the width of the row numbers columns
        rowNum: 60,
        pager: "#jqGridPager",
        cellsubmit: "clientArray", //当单元格发生变化后不直接发送请求、"remote"默认直接发送请求

        gridComplete: function () {
            var gridName = "ProjectGrid";
            Merger(gridName, 'ProjectNo');
            MergerSpecifiedLines(gridName, "MoldNo", 3);
            MergerSpecifiedLines(gridName, "Memo", 3);
            //for (var i = $("#ProjectGrid").rows.length - 1; i > 0; i--) {
            //    $("#ProjectGrid").getcell()
            //}
        },
        ondblClickRow: function () {
            if (dept == 1) {
                var id = $("#ProjectGrid").getCell($("#ProjectGrid").getGridParam("selrow"), "ID");
                location.href = "/Project/Edit?ProjectID=" + id;
            }
        },

        loadComplete: function () {
            $(".jqgrow", this).contextMenu("ProjectContextMenu", {
                bindings: {
                    //Go to the create new mold project page
                    'AddProject': function () {
                        var pjID=GetCurrentID("ProjectGrid");
                        location.href = "/Project/Edit?ParentID=" + pjID;
                                      
                    },
                    'Milestone': function () {
                        ShowPhaseModification(GetCurrentID("ProjectGrid"));
                    },
                    'FinishPhase': function () {
                        FinishPhase(GetCurrentID("ProjectGrid"));
                    },
                    'AddMemo': function () {
                        $("#MemoProject").val(GetCurrentID("ProjectGrid"));
                        $("#Memo").val(GetMemo("ProjectGrid"));
                        $("#ProjectMemo").modal("show");
                    },
                    'PauseProject': function () {
                        var row = GetCurrentID("ProjectGrid");
                        ShowPauseProject(row);
                    },
                    'SetFile': function (irow) {
                        var row = GetCurrentID("ProjectGrid");
                        ShowProjectFile(row);
                    },
                    'MoldFixProject': function () {
                        var row = GetCurrentID("ProjectGrid");
                        CreateMoldFixProject(row);
                    },
                    'ProjectHistory': function () {
                        var row = GetCurrentID("ProjectGrid");
                        ShowProjectHistory(row);
                    },
                    'PhaseTask': function () {
                        var row = GetCurrentID("ProjectGrid");
                        PhaseTask(row);
                    }

                },
                onContextMenu: function (event/*, menu*/) {
                    $("#ProjectGrid").jqGrid("setSelection", $(event.target).closest("tr.jqgrow").attr("id"));
                    var item = $(event.target).closest("td");
                    $("#selPhase").val(item[0].id);
                    var pjID = GetCurrentID("ProjectGrid");
                    console.log(pjID);
                    $.ajax({
                        url: "/Project/IsMainPJ?pjID=" + pjID,
                        type: 'get',
                        async: false,
                        success: function (res) {
                            if (res == 'True') {
                                //$('#AddProject').css('height', '28px');
                                //$('#AddProject').css('display', 'inline');
                                //$('#AddProject span').css({ 'height': '15px', 'font-size': '12px' });
                                //$('#AddProject img').css('height', '20px');
                                $('#jqContextMenu').css('display', 'block');
                                $('#AddProject').css('pointer-events', 'auto');
                            }
                            else {
                                $('#jqContextMenu').css('display', 'block');
                                $('#AddProject').css('pointer-events', 'none');
                                //$('#AddProject').css('display', 'none');
                                //$('#AddProject span').css({ 'height': '0px', 'font-size': '0px' });
                                //$('#AddProject img').css('height', '0px');
                            }

                        }
                    })
                    return true;
                },
                onShowMenu: function (e, menu) {//显示菜单
                    return menu;
                },
            });
        },
    });
}

//设备信息
function MachinesInfoGrid() {
    var _url = "/Administrator/Service_Json_GetMachinesInfo";
    $("#tb_MachinesInfo").jqGrid({
        url: _url,
        mtype: "post",
        styleUI: 'Bootstrap',
        datatype: "json",
        height: document.documentElement.clientHeight * 0.7,
        width: document.body.clientWidth * 0.8,
        colModel: [
            { label: '设备代码', name: 'MachineCode', width: 120 },
            { label: '设备名称', name: 'MachineName', width: 120 },
            { label: '设备品牌', name: 'EquipBrand', width: 120 },           
            { label: '部门', name: 'DepName', width: 120},
            { label: '工艺类型', name: 'TaskTypeName', width: 120},
            { label: '产能(min)', name: 'Capacity', width: 120 },
            { label: '计划停机时间(min)', name: 'Downtime', width: 120 },
            { label: '机器费率', name: 'Cost', width: 120 },
            { label: '可用', name: 'Status', width: 120, formatter: 'checkbox' },
            { label: '部门ID', name: 'DepID', width: 120,hidden:true },
        ],
        //cellEdit: true,
        autoScroll: true,
        multiselect: true,
        cellsubmit: "clientArray", //当单元格发生变化后不直接发送请求、"remote"默认直接发送请求
        ondblClickRow: function () {
            var row = GetDblClickRow('#tb_MachinesInfo');
            ShowEditMInfo(row);
        }
    })
}

//设置任务开始
function TaskStartGrid(_TaskIDs) {
    var _url = "/Task/Service_Json_GetTaskByIDs?TaskIDs=" + _TaskIDs;
    $("#tb_TaskStart").jqGrid({
        url: _url,
        styleUI: 'Bootstrap',
        datatype: "json",
        mtype: "post",
        width: 550,
        colModel: [
            { label: '任务ID', name: 'ID', width: 120, hidden: true },
            { label: '任务名', name: 'TaskName', width: 120 },
            { label: '状态', name: 'State', width: 120 },
            { label: 'MachinesCode', name: 'MachinesCode', width: 120, hidden: true },
            { label: '机器', name: 'MachinesName', width: 120 },
            { label: '人员', name: 'UserName', width: 120 },
            { label: 'wsUserID', name: 'UserID', width: 120, hidden: true },
            { label: '加工时间(min)', name: 'TotalTime', width: 120, formatter: 'integer', hidden: true }
        ],
        autoScroll: true,
        multiselect: true,
        cellsubmit: "clientArray", //当单元格发生变化后不直接发送请求、"remote"默认直接发送请求
    })
}

//设置任务结束
function WFTaskFinishGrid(_TaskIDs) {
    var _url = "/Task/Service_Json_GetTaskByIDs?TaskIDs=" + _TaskIDs;
    $("#tb_SetupWFTaskHour").jqGrid({
        url: _url,
        styleUI: 'Bootstrap',
        datatype: "json",
        mtype: "post",
        width: 420,
        colModel: [
            { label: '任务ID', name: 'ID', width: 30, hidden: true },
            { label: '任务名', name: 'TaskName', width: 120 },
            { label: '状态', name: 'State', width: 60 },
            { label: 'MachinesCode', name: 'MachinesCode', width: 30, hidden: true },
            { label: '机器', name: 'MachinesName', width: 120, hidden: true },
            { label: '人员', name: 'UserName', width: 60 },
            { label: 'wsUserID', name: 'UserID', width: 60, hidden: true },
            { label: '加工时间(min)', name: 'TotalTime', width: 100, formatter: 'integer', editable: true, }
        ],
        cellEdit: true,
        autoScroll: true,
        //multiselect: true,
        cellsubmit: "clientArray", //当单元格发生变化后不直接发送请求、"remote"默认直接发送请求
        onCellSelect: function (rowid, iCol, cellcontent, event) {
            //列 TotalTime
            if (iCol == 7) {
                var rowState = $("#tb_SetupWFTaskHour").getCell(rowid, "State");
                if (rowState == '外发') {
                    $("#tb_SetupWFTaskHour").jqGrid('setCell', rowid, iCol, '', 'edit-cell');
                }
                else {
                    //加工时间为0 时可以编辑
                    cellcontent = Number(cellcontent);
                    if (cellcontent > 0) {
                        $("#tb_SetupWFTaskHour").jqGrid('setCell', rowid, iCol, '', 'not-editable-cell');
                    } else {
                        $("#tb_SetupWFTaskHour").jqGrid('setCell', rowid, iCol, '', 'edit-cell');
                    }
                }
            }           
        },
    })
}
//零件列表
function PartListGrid(MoldID, Height) {
    $("#PartGrid").jqGrid({
        //url: MoldID == 0 ? "" : "/Part/JsonParts?MoldNumber=" + MoldID,
        mtype: "GET",
        styleUI: 'Bootstrap',
        datatype: "json",
        colModel: [
            { label: 'No.', name: 'Index', width: 37 },
            { label: '', name: 'ID', width: 75, key: true, hidden: true },
            { label: '零件短名', name: 'ShortName', width: 168, sorttable: true, sorttype: 'Text' },
            { label: '版本', name: 'Version', width: 40, sorttable: false, sorttype: 'Text' },
            { label: '物料编号', name: 'MaterialNo', width: 93 },
            { label: '尺寸或规格', name: 'Specification', width: 265 },
            { label: '零件号', name: 'JobNo', width: 53 },
            { label: '材料', name: 'Material', width: 80, sortable: false, sorttype: "text" },
            { label: '数量', name: 'Quantity', width: 40 },
            { label: '附加数量', name: "AppendQty", width: 40 },
            { label: '库存', name: 'InStock', width: 40, hidden: true },
            { label: '硬度', name: 'Hardness', width: 77 },
            { label: '品牌', name: 'Supplier', width: 100 },
            { label: '详图', name: 'DetailDrawing', formatter: "checkbox", width: 40 },
            { label: '略图', name: 'BriefDrawing', formatter: "checkbox", width: 40 },
            { label: '附图订购', name: 'PUDrawing', formatter: "checkbox", width: 40 },
            { label: '追加工', name: 'AdditionalMachining', formatter: "checkbox", width: 40 },
            { label: '备注', name: 'Memo', width: 158 },
            { label: '创建日期', name: 'Drawing', width: 80 },
            { label: 'UG', name: 'FromUG', formatter: "checkbox", width: 40, hidden: true },
            { label: '采购', name: 'InPurchase', formatter: "checkbox", width: 40 },
            { label: 'ERP料号', name: 'ERPPartID', width: 97,hidden:true }
        ],
        //索引
        viewrecords: true,
        height: Height,
        //width: document.body.clientWidth * 0.8,
        multiselect: true,
        sortname: 'PartName',
        sortorder: 'asc',
        loadonce: true,
        autoScroll: true,
        //rownumbers: true, // show row numbers
        //rownumWidth: 25, // the width of the row numbers columns
        rowNum: 1000,
        scroll: true,
        //分页
        //pager: "#jqGridPager",
        cellsubmit: "clientArray", //当单元格发生变化后不直接发送请求、"remote"默认直接发送请求
        onSelectRow: function (ids) {

        },
        //updated by michael
        ondblClickRow: function () {
            var _id = GetDblClickCell("PartGrid", "ID");
            var _url = "/Part/GetIsUpgrade";
            var _partlistID = $('#sltVersion').val();
            if (_partlistID != null) {
                $.ajax({
                    url: "/Part/GetIsUpgrade?PartListID=" + _partlistID,
                    dataType: "html",
                    method: "Get",
                    success: function (msg) {
                        if (Number(msg) > 1) {
                            $("#Specification").attr("disabled", true);
                            $("#MaterialID").attr("disabled", true);
                            $("#Quantity").attr("disabled", true);
                            $("#BrandID").attr("disabled", true);
                            LoadPart(_id);
                        }
                    }
                });
            }
            else {
                alert('请选择版本！');
            }
        },
        loadComplete: function () {
            var ids = $("#PartGrid").getDataIDs();
            for (var i = 0; i < ids.length; i++) {
                var rowData = $("#PartGrid").getRowData(ids[i]);
                if (rowData.FromUG == 'No') {
                    $('#' + ids[i]).find("td").addClass("ManualPart");
                }
            }
            //$(".jqgrow", this).contextMenu("PartContextMenu", {
            //    bindings: {
            //        //Go to the create new mold project page
            //        'Edit': function () {
            //            var _id = GetCurrentID("PartGrid");
            //            LoadPart(_id);
            //        },
            //        'Delete': function () {
            //        },
            //        'Memo': function () {
            //        },
            //        'Revision': function () {

            //        }
            //    },
            //    onContextMenu: function (event/*, menu*/) {
            //        //Set the mouse hovered line as selected status
            //        if (GetCurrentID("PartGrid") == undefined) {
            //            $("#PartGrid").jqGrid("setSelection", $(event.target).closest("tr.jqgrow").attr("id"));
            //        }

            //        return true;
            //    }
            //});
        },
        gridComplete: function () {

        }
    });
    //$(grid).closest(".ui-jqgrid-bdiv").css({ 'overflow-y': 'scroll' });
}

//零件查询
function PartSearchGrid() {
    var _url = "/Part/Service_Json_GetPartByKeys";
    $("#tb_PartSearch").jqGrid({
        url: _url,
        styleUI: 'Bootstrap',
        datatype: "json",
        width: 650,
        colModel: [
            { label: 'PartID', name: 'PartID', width: 80, hidden: true },
            { label: '零件短名', name: 'ShortName', width: 80 },
            { label: '物料编号', name: 'PartNumber', width: 80 },
            { label: '规格', name: 'Specification', width: 80},
            { label: '材料', name: 'MaterialName', width: 80 },
            { label: '品牌', name: 'BrandName', width: 120 },
            { label: '数量', name: 'Quantity', width: 60 ,formatter:'number'},
            { label: '版本', name: 'Version', width: 60 },
        ],
        autoScroll: true,
        multiselect: true,
        cellsubmit: "clientArray", //当单元格发生变化后不直接发送请求、"remote"默认直接发送请求
    })
}

//CAM任务列表
function CAMTaskList(MoldNumber, TaskType, State, CAM) {
    $("#TaskGrid").jqGrid({
        url: '/Task/JsonMachineTasks?MoldNumber=' + MoldNumber + "&TaskType=" + TaskType + "&State=" + State + "&CAM=" + CAM,
        mtype: "GET",
        styleUI: 'Bootstrap',
        datatype: "json",
        colModel: [
            { label: "", name: "ID", hidden: true },
            { label: '图纸', name: 'DrawingFile', width: 40 },
            { label: '任务名', name: 'TaskName', width: 150, sorttype: 'string' },
            { label: '版本', name: 'Version', width: 40 },
            { label: 'CAD文档', name: 'CADPartName', width: 120, hidden: true },//WEDM图纸历史
            { label: 'CAD', name: 'CAD', width: 60, hidden: true },
            { label: '备注', name: 'Memo', width: 75 },
            //WEDM图纸历史
            { label: '加工精度', name: 'Preciston', width: 120, hidden: true },
            { label: '特征数量', name: 'FeatureCount', width: 120, hidden: true },
            { label: '长度', name: 'Length', width: 75, hidden: true },
            { label: '厚度', name: 'Thickness', width: 75, hidden: true },
            //----
            { label: '时间', name: 'Time', width: 40, hidden: true },
            { label: '状态', name: 'State', width: 75, hidden: true },
            { label: '状态备注', name: 'StateMemo', width: 75, hidden: true },
            //CNC
            { label: '毛坯', name: 'Raw', width: 75, hidden: true },
            { label: '型号', name: 'Model', width: 75, hidden: true },
            { label: "表面", name: "Surface", width: 75, hidden: true },
            { label: "平动", name: "Obit", width: 60, hidden: true },
            { label: "电极位置", name: "ELEPos", width: 300, hidden: true },
            { label: 'R', name: 'R', width: 30, hidden: true },
            { label: 'F', name: 'F', width: 30, hidden: true },
            //----
            { label: '数量', name: 'Quantity', width: 30, hidden: true },
            { label: '备料', name: 'Prepared', formatter: "checkbox", width: 40, hidden: true },
            //MG
            { label: '材料', name: 'Material', width: 75, hidden: true },
            { label: 'HRC', name: 'HRC', width: 75, hidden: true },
            //----
            { label: '工艺', name: 'ProcessName', width: 100, hidden: true },
            { label: '实际工时', name: 'ActualTime', width: 120, hidden: true, formatter: "number" },
            { label: '优先', name: 'Priority', width: 40, hidden: true },
            //日期
            { label: '创建日期', name: 'CreateTime', width: 75, hidden: true },
            { label: '计划日期', name: 'PlanTime', width: 75, hidden: true },
            { label: '接收日期', name: 'AcceptTime', width: 75, hidden: true },
            { label: '发布日期', name: 'ReleaseTime', width: 75, hidden: true },//图纸历史
            { label: '开始日期', name: 'StartTime', width: 75, hidden: true },
            { label: '结束日期', name: 'FinishTime', width: 120, hidden: true },
            { label: '预计日期', name: 'ForecastTime', width: 75, hidden: true },
            //----
            //WEDM图纸历史
            { label: 'CAM', name: 'CAM', width: 60, hidden: true },
            { label: "QC点", name: "QCPoints", width: 40, formatter: "checkbox", hidden: true },
            { label: '跑位检查', name: "PosCheck", width: 40, formatter: "checkbox", hidden: true },
            { label: 'QC', name: 'QC', width: 60, hidden: true },
            //----                     
            { label: '加工', name: 'Workshop', width: 60, hidden: true },
            { label: '机器号', name: 'Machine', width: 75, hidden: true },
            { label: '操作人员', name: 'Operater', width: 120, hidden: true },
        ],
        viewrecords: true,
        multiselect: true,
        height: document.documentElement.clientHeight - 203,
        width: document.body.clientWidth * 0.8,
        rowNum: 500,
        //shrinkToFit: false,
        loadonce: true,
        ondblClickRow: function (iRow) {
            EditTask(iRow);
        },
        loadComplete: function () {
            $(".jqgrow", this).contextMenu("CAMTaskContextMenu", {
                bindings: {
                    'TaskInfo': function () {
                        var _id = GetCurrentID("TaskGrid");
                        EditTaskInfo(_id);
                    },
                    'ReleaseCAMTask': function () {
                        var ids = GetMultiSelectedIDs("TaskGrid");
                        if (ids.length <= 0) {
                            alert('请选择需要发布的任务！');
                            return;
                        }                          
                        ReleaseCAMTask(ids);
                    },

                    'DisableCAMTask': function () {
                        if (confirm("确认删除任务？")) {
                            DeleteTask(GetCurrentID("TaskGrid"));
                        }

                    },
                    'PauseTask': function () {
                        if (confirm("确认暂停任务？")) {
                            var _id = GetCurrentID("TaskGrid");
                            PauseTask(_id);
                        }
                    },
                    'DeleteTask': function () {
                        if (confirm("确认取消任务？")) {
                            var _id = GetCurrentID("TaskGrid");
                            DeleteTaskByCAM(_id);
                        }
                    },
                    'NewTask': function () {
                        
                    }
                },
            });
        },

    });
}

//MG升版
function MGUptList(IDs) {
    var _url = "/Task/JsonMGUptTaskList?TaskIDs=" + IDs;
    $("#MGUptVerTb").jqGrid({
        url: _url,
        mtype: "post",
        styleUI: 'Bootstrap',
        datatype: "json",
        width:550,
        colModel: [
            { label: 'TaskID', name: 'TaskID', width: 20,hidden:true },
            { label: '任务名', name: 'TaskName', width: 120 },
            { label: 'CAM', name: 'CAM', width: 120},
            { label: '工艺名(可编辑)', name: 'Technology', width: 120, editable: true, edittype: 'select', editoptions: { value: { 0: '铣床', 1: '磨床', 2: '铣磨', 3: '车' } } },//, edittype: 'select', editoptions: {value: {1:'铣床', 2:'磨床', 3:'铣磨',4:'车'}}
            { label: '数量(可编辑)', name: 'Qty', width: 120, editable: true, edittype: 'text',formatter:'number' },//需要胡工同意
            { label: '图纸', name: 'DrawingFile', width: 120 }
        ],
        cellEdit: true,
        cellsubmit: "clientArray", //当单元格发生变化后不直接发送请求、"remote"默认直接发送请求
    })
}

//TODO: 加工任务列表 表配置
function TaskList(MoldNumber, TaskType, State, InPage) {
    var _height;
    var _width;
    var _url;
    if (InPage == undefined) {
        _height = document.documentElement.clientHeight - 203;
        _width = document.body.clientWidth * 0.875
    } else {
        _height = (document.documentElement.clientHeight - 220) * 0.385;
        _width = document.body.clientWidth * 0.6;
    }
    if (MoldNumber == "") {
        _url = "";
    } else {
        _url = '/Task/JsonMachineTasks?MoldNumber=' + MoldNumber + "&TaskType=" + TaskType + "&State=" + State;
    }

    $("#TaskGrid").jqGrid({
        url: _url,
        mtype: "GET",
        styleUI: 'Bootstrap',
        datatype: "json",
        colModel: [
            { label: "", name: "ID", hidden: true },
            { label: '图纸', name: 'DrawingFile', width: 40 },
            { label: '任务名', name: 'TaskName', width: 150, sorttype: 'string' },
            { label: '版本', name: 'Version', width: 40 },
            { label: 'CAD文档', name: 'CADPartName', width: 120, hidden: true },//WEDM图纸历史
            { label: 'CAD', name: 'CAD', width: 60, hidden: true },
            { label: '备注', name: 'Memo', width: 75 },            
            //WEDM图纸历史
            { label: '加工精度', name: 'Preciston', width: 120, hidden: true },
            { label: '特征数量', name: 'FeatureCount', width: 120, hidden: true },
            { label: '长度', name: 'Length', width: 75, hidden: true },
            { label: '厚度', name: 'Thickness', width: 75, hidden: true },
            //----
            { label: '时间', name: 'Time', width: 40, hidden: true },
            { label: '状态', name: 'State', width: 75, hidden: true },
            { label: '状态备注', name: 'StateMemo', width: 75, hidden: true },
            //CNC
            { label: '毛坯', name: 'Raw', width: 75, hidden: true },
            { label: '型号', name: 'Model', width: 75, hidden: true },

            { label: "表面", name: "Surface", width: 75, hidden: true },
            { label: "平动", name: "Obit", width: 60, hidden: true },
            { label: "电极位置", name: "ELEPos", width: 300, hidden: true },

            { label: 'R', name: 'R', width: 30, hidden: true },
            { label: 'F', name: 'F', width: 30, hidden: true },
            //----
            { label: '数量', name: 'Quantity', width: 30, hidden: true },
            { label: '备料', name: 'Prepared', formatter: "checkbox", width: 40, hidden: true },
            //MG
            { label: '材料', name: 'Material', width: 75, hidden: true },
            { label: 'HRC', name: 'HRC', width: 75, hidden: true },
            //----
            { label: '工艺', name: 'ProcessName', width: 100, hidden: true },
            { label: '实际工时', name: 'ActualTime', width: 120, hidden: true, formatter: "integer"},
            { label: '优先', name: 'Priority', width: 40, hidden: true },
            //日期
            { label: '创建日期', name: 'CreateTime', width: 75, hidden: true },
            { label: '计划日期', name: 'PlanTime', width: 75, hidden: true },
            { label: '接收日期', name: 'AcceptTime', width: 75, hidden: true },
            { label: '发布日期', name: 'ReleaseTime', width: 75, hidden: true },//图纸历史
            { label: '开始日期', name: 'StartTime', width: 75, hidden: true },
            { label: '结束日期', name: 'FinishTime', width: 120, hidden: true },
            { label: '预计日期', name: 'ForecastTime', width: 75, hidden: true },
            //----
            //WEDM图纸历史
            { label: 'CAM', name: 'CAM', width: 60, hidden: true },
            { label: "QC点", name: "QCPoints", width: 40, formatter: "checkbox", hidden: true },
            { label: '跑位检查', name: "PosCheck", width: 40, formatter: "checkbox", hidden: true },
            { label: 'QC', name: 'QC', width: 60, hidden: true },
            //----                     
            { label: '加工', name: 'Workshop', width: 60, hidden: true },                        
            { label: '机器号', name: 'Machine', width: 75, hidden: true },
            { label: '操作人员', name: 'Operater', width: 120, hidden: true },
        ],
        viewrecords: true,
        height: _height, //document.documentElement.clientHeight - 220,
        width: _width, //document.body.clientWidth * 0.875,
        rowNum: 500,
        //sortname: 'Priority',
        //sortable:true, 
        multiselect: true,
        loadonce: true,
        //shrinkToFit: false,
        loadComplete: function () {
            $(".jqgrow", this).contextMenu("MachineTaskContextMenu", {
                bindings: {
                    //Go to the create new mold project page
                    'AcceptCAMTask': function () {
                        var _id = GetCurrentID("TaskGrid");
                        AcceptCAMTask(_id);
                    },
                    'ReleaseCAMTask': function () {
                        var _id = GetCurrentID("TaskGrid");
                        ReleaseCAMTask(_id);
                    },
                    'EditCNCTask': function () {

                        ShowCNCItemList();
                    },
                    'ELECompensation': function () {
                        var _id = GetCurrentID("TaskGrid");
                        EditEleCompensation(_id);
                    }
                },
            });
        },

    });
}


///CNC任务信息编辑
function CNCItemList(TaskIDs) {
    var _url;
    $("#TaskItemGrid").jqGrid({
        url: "/task/JsonCNCTaskItems?TaskIDs=" + TaskIDs,
        mtype: "GET",
        styleUI: 'Bootstrap',
        datatype: "json",
        colModel: [
            { label: "", name: "CNCItemID", hidden: true },
            { label: '电极名', name: 'LabelName', width: 150 },
            { label: '准备', name: 'Ready', formatter: "checkbox", width: 40 },
			{ label: '需要加工', name: "Required", formatter: "checkbox", width: 40 },
            { label: '材料', name: 'Material', width: 75 },
            { label: '安全高度', name: 'SafetyHeight', width: 40 },
            { label: '毛坯', name: 'Raw', width: 75 },
            { label: '标签打印', name: 'LabelPrinted', formatter: "checkbox", width: 40 },
            { label: '已完成', name: 'Finished', width: 75, formatter: "checkbox" },
        ],
        viewrecords: true,
        height: document.documentElement.clientHeight - 80,
        width: document.body.clientWidth * 0.95,
        rowNum: 500,
        loadonce: true,
        multiselect: true,
        loadComplete: function () {
            $(".jqgrow", this).contextMenu("CNCItemContextMenu", {
                bindings: {
                    'TaskReady': function () {
                        CNCTaskReady();
                    },
                    "Required": function () {
                        var ids = GetMultiSelectedCell("TaskItemGrid", "CNCItemID");
                        SetItemRequired(ids);
                    },
                    "NotRequired": function () {
                        var ids = GetMultiSelectedCell("TaskItemGrid", "CNCItemID");
                        SetItemNotRequired(ids);
                    }
                },
            });

        }
    })
}

function EDMItemList(TaskIDs) {
    var _url;
    //if (TaskIDs == "") {
    //    _url = "";
    //} else {
    //    _url = ""
    //}

    $("#TaskGrid").jqGrid({
        //url: _url,
        mtype: "GET",
        styleUI: 'Bootstrap',
        datatype: "json",
        colModel: [
            { label: "", name: "ID", hidden: true },
            { label: "", name: "ELEName", hidden: true },
            { label: '名称', name: 'LabelName', width: 80 },
            { label: '间隙', name: 'Gap', width: 50 },
            { label: 'X', name: 'OffsetX', width: 50 },
            { label: 'Y', name: 'OffsetY', width: 50 },
            { label: 'Z', name: 'OffsetZ', width: 50 },
            { label: 'C', name: 'OffsetC', width: 50 },
            { label: '间隙补偿', name: 'GapCompensate', width: 50 },
            { label: '高度补偿', name: 'ZCompensate', width: 50 },
            { label: '表面', name: 'Surface', width: 50 },
            { label: '平动', name: 'Obit', width: 50 },
            { label: '材料', name: 'Material', width: 50 },
            { label: '', name: 'ElePoints', hidden: true },
            { label: '', name: 'EleType', hidden: true },
            { label: '', name: 'StockGap', hidden: true },
            { label: '', name: 'CNCMachMethod', hidden: true }

        ],
        viewrecords: true,
        height: (document.documentElement.clientHeight - 285) / 2,
        width: document.body.clientWidth * 0.8,
        rowNum: 500,
        loadonce: true,
        multiselect: true,
    })
}

function EDMCurrentItemList() {
    $("#ProcessGrid").jqGrid({
        mtype: "GET",
        styleUI: 'Bootstrap',
        datatype: "json",
        colModel: [
            { label: "", name: "ID", hidden: true },
            { label: "", name: "ELEName", hidden: true },
            { label: '名称', name: 'LableName', width: 100 },
            { label: '位置', name: 'Position', width: 30 },
            { label: '间隙', name: 'Gap', width: 50 },
            { label: 'X', name: 'OffsetX', width: 40 },
            { label: 'Y', name: 'OffsetY', width: 40 },
            { label: 'Z', name: 'OffsetZ', width: 40 },
            { label: 'C', name: 'OffsetC', width: 40 },
            { label: '间隙补偿', name: 'GapCompensate', width: 50 },
            { label: '高度补偿', name: 'ZCompensate', width: 50 },
            { label: '表面', name: 'Surface', width: 50 },
            { label: '平动', name: 'Obit', width: 50 },
            { label: '材料', name: 'Material', width: 50 },
            { label: '', name: 'ElePoints', hidden: true },
            { label: '', name: 'EleType', hidden: true },
            { label: '', name: 'StockGap', hidden: true },
            { label: '', name: 'CNCMachMethod', hidden: true }
        ],
        viewrecords: true,
        height: (document.documentElement.clientHeight - 285) / 2,
        width: document.body.clientWidth * 0.8,
        rowNum: 500,
        multiselect: true,
        loadComplete: function () {
            $(".jqgrow", this).contextMenu("EDMItemContextMenu", {
                bindings: {
                    "ItemPosition": function () {
                    },
                    "ItemSurface": function () {
                    },
                    "Compensation": function () {
                    },
                    "ObitSelect": function () {
                    },
                    "MaterialSelect": function () {
                    },
                },
                onContextMenu: function (event/*, menu*/) {
                    //Set the mouse hovered line as selected status                    
                    $("#ProcessGrid").jqGrid("setSelection", $(event.target).closest("tr.jqgrow").attr("id"));
                    return true;
                }
            });
        },
    });
}

//库存内容列表
function StockItem(Keyword, MoldNumber, PurchaseType, Mode) {
    var _url = "/Warehouse/JsonWarehouseStock?keyword=" + Keyword + "&MoldNumber=" + MoldNumber + "&PurchaseType=" + PurchaseType;
    var _multi;
    if (Mode == undefined) {
        if (PurchaseType == 2) {
            _multi = true;
        } else {
            _multi = true;
        }
    } else {
        _multi = true;
    }

    $("#StockItemGrid").jqGrid({
        url: _url,
        mtype: "GET",
        styleUI: 'Bootstrap',
        datatype: "json",
        colModel: [
            { label: "", name: "WarehouseStockID", hidden: true },
            { label: '零件名', name: 'Name', width: 180 },
            { label: '物料号', name: 'PartNumber', width: 75 },
            { label: '规格', name: 'Specification', width: 75 },
            { label: "材料", name: "Material", width: 50 },
            { label: '库存', name: 'Quantity', width: 60 },
            { label: '安全库存', name: 'SafeQuantity', width: 80, hidden: true },
            { label: '供应商', name: "SupplierName", width: 80, hidden: true },
            { label: '采购类型', name: "PurchaseType", width: 80, hidden: true },
            { label: '备库类型', name: 'StockType', width: 80, hidden: true },
            { label: '计划时间', name: "PlanTime", width: 80, hidden: true },
            { label: '预计时间', name: "ForecastTime", width: 80, hidden: true },
            { label: '结束时间', name: "FinishTime", width: 80, hidden: true },
            { label: '生成时间', name: "CreateTime", width: 80, hidden: true },
            { label: '入库时间', name: "InstockTime", width: 80 },
            { label: '生成人员', name: "PurchaseUser", wdith: 80, hidden: true },
            { label: "状态", name: "State", width: 60, hidden: true },
            { label: "到库", name: "ReceiveQty", width: 60, hidden: true },
            { label: "领出", name: "OutQty", width: 60, hidden: true },
            { label: '计划数量', name: 'PlanQty', width: 80, hidden: true },
            { label: "仓库", name: "Warehouse", width: 80 },
            { label: "库位", name: "WarehousePosition", width: 80 },

        ],
        viewrecords: true,
        height: document.documentElement.clientHeight - 220,
        width: document.body.clientWidth * 0.875,
        rowNum: 500,
        multiselect: _multi,
        loadonce: true,
        shrinkToFit: false,
        autoScroll: true,
        loadComplete: function () {
            $(".jqgrow", this).contextMenu("StockItemContext", {
                bindings: {

                    "StockEdit": function () {
                        var _ids = GetMultiSelectedCell("StockItemGrid", "WarehouseStockID");
                        var _id = _ids.split(',');
                        EditStockItem(_id[0]);
                    },
                    "StockPurchase": function () {
                        var _ids = GetMultiSelectedCell("StockItemGrid", "WarehouseStockID");
                        location.href = "/Purchase/PRDetail?WarehouseStockIDs=" + _ids;
                    },
                    "SafeQtyEdit": function () {
                        var _ids = GetMultiSelectedCell("StockItemGrid", "WarehouseStockID");
                        if (_ids != "") {
                            $("#SafeStockSetting").modal("show");
                        } else {
                            alert("请至少选择一种备库零件");
                        }
                    },
                    "DeleteStock": function () {
                        var _ids = GetMultiSelectedCell("StockItemGrid", "WarehouseStockID");
                        if (_ids != "") {
                            if (confirm("确认删除选中备库零件？")) {
                                DeleteStock(_ids);
                            }
                        } else {
                            alert("请选择要删除的备库零件");
                        }
                    },
                    "PositionEdit": function () {
                        var _ids = GetMultiSelectedCell("StockItemGrid", "WarehouseStockID");
                        if (_ids != "") {
                            ShowWarehousePositionEditDialog(_ids);
                        } else {
                            alert("请选择要修改库位的零件");
                        }
                    }

                },
                onContextMenu: function (event/*, menu*/) {
                    ////Set the mouse hovered line as selected status                    
                    //$("#StockItemGrid").jqGrid("setSelection", $(event.target).closest("tr.jqgrow").attr("id"));
                    return true;
                }
            });
        }
    })
}

//采购申请单列表
//function PRListGrid(MoldNumber, PRKeyword, StartDate, FinishDate, Supplier, PurchaseType, Department, State) {
function PRListGrid(Department, State) {
    var _url = "/Purchase/JsonPRList?State=" + State + "&Department=" + Department;
    var _condition = "";

    _url = _url + _condition;
    $("#PRListGrid").jqGrid({
        url: _url,
        mtype: "Get",
        styleUI: "Bootstrap",
        datatype: "json",
        colModel: [
            { label: "", name: "ID", hidden: true },
            { label: '单号', name: 'PRNumber', width: 75 },
            //{ label: '项目号', name: 'ProjectNumber', width: 75 },
            //{ label: '模具号', name: 'MoldNumber', width: 75 },
            { label: '创建人', name: 'PRUser', width: 75 },
            { label: '所属部门', name: 'Department', width: 75 },
            { label: '发起日期', name: 'CreateDate', width: 75, sorttype: 'date' },
            { label: '提请人', name: 'SubmitUser', width: 75 },
            { label: "审批人", name: "ReviewUser", width: 75 },
            { label: '审批日期', name: 'ReviewDate', width: 75, sorttype: 'date' },
            { label: '采购类型', name: "PurchaseType", width: 75 },
            { label: '状态', name: 'Status', width: 75 },
            { label: "备注", name: "Memo", width: 100 },
            { label: '申请人', name: 'ApprovalUser', width: 75 },
            {label: 'ERP料号同步', name: 'ERPPartStatus', width: 75, 
            formatter: function (cellvalue, options, rowObject) {
                var status;
                if (cellvalue == 'True')
                    status='已同步'
                else
                    status = '未同步'
                return status;
            }            
            },
        ],
        viewrecords: true,
        height: document.documentElement.clientHeight - 230,
        width: document.body.clientWidth * 0.98,
        rowNum: 500,
        loadonce: true,
        multiselect: true,
        ondblClickRow: function (iRow) {

            location.href = "/Purchase/PRDetail?PurchaseRequestID=" + GetDblClickCell("PRListGrid", "ID");
        }
    })
}

//申请单内容列表
function PRContentGrid(PartIDs, PRID, state, TaskIDs, WHIDs) {
    var _url;
    var _type=0;
    if (PartIDs != "") {
        //零件请购
        _type = 1;
        _url = "/Purchase/JsonPRNew?PartIDs=" + PartIDs;
    } else if (TaskIDs != "") {
        //外发任务请购
        _type = 2;
        _url = "/Purchase/JsonPROutSource?TaskIDs=" + TaskIDs ;
    } else if (PRID != 0) {
        //请购明细查询
        _type = 10;
        _url = "/Purchase/JsonPRDetail?PRID=" + PRID;
    } else if (WHIDs != "") {
        //备库请购
        _type = 3;
        _url = "/Purchase/JsonPRWarehouseStock?WarehouseStockIDs=" + WHIDs;
    } else {
        _url = "";
    }

    $("#PRContentGrid").jqGrid({
        url: _url,
        mtype: "GET",
        styleUI: 'Bootstrap',
        datatype: "json",
        colModel: [
            { label: "", name: "ID", hidden: true },
            { label: "", name: "PartID", hidden: true },//, hidden: true
            { label: "", name: "TaskID", hidden: true },
            { label: "", name: "WarehouseStockID", hidden: true },
            { label: '零件短名', name: 'Name', width: 120 },
            { label: '数量', name: 'Quantity', width: 50 },
            { label: '物料编号', name: 'PartNumber', width: 150 },
            { label: '规格', name: 'Specification', width: 260 },
            { label: '材料', name: 'Material', width: 75 },
            { label: '硬度', name: 'Hardness', width: 75 },
            { label: '零件号', name: 'JobNo', width: 75 },
            { label: '品牌', name: 'BrandName', width: 75, hidden: true },
            { label: '供应商', name: "SupplierName", width: 150 },
            { label: '附图', name: 'Drawing', formatter: "checkbox", width: 75 },

            { label: '状态', name: 'State', width: 75 },
            { label: "", name: "PurchaseItemID", hidden: true },
            { label: '需求日期', name: "RequireTime", width: 150 },
            { label: "", name: "MoldNumber", hidden: true },
            { label: "", name: "CostCenterID", hidden: true },
            { label: "归属部门", name: "CostCenterName" },
            { label: 'ERP料号', name: "ERPPartID", width: 100, hidden: true },
            { label: '备注', name: 'Memo', width: 150 },
            { label: '人员', name: 'Operater', width: 150, hidden: true },
            { label: '设备', name: 'MachineName', width: 150, hidden: true },
            { label: 'MachineCode', name: 'MachineCode', width: 150, hidden: true },
        ],
        viewrecords: true,
        height: document.documentElement.clientHeight - 200,
        width: document.body.clientWidth * 0.875,
        rowNum: 500,
        multiselect: true,
        loadonce: true,
        ondblClickRow: function (iRow) {
            if (state < 2) {
                $("#row").val(iRow);
                var row = $(event.target).closest("tr.jqgrow").attr("id");
                var _id = $("#PRContentGrid").getCell(row, "ID");
                $('#ViewType').val(_type.toString());
                EditPrContent(_id, row);
            }
        }
    })
}

//仓库收货订单列表
function WHPOListGrid(ProjectID, State, UserID) {
    var _url = "/Warehouse/JsonPurchaseOrder";
    var _condition = "";
    if (ProjectID != 0) {
        _condition = _condition + "?ProjectID=" + ProjectID;
    }
    if (State != 0) {
        _condition = _condition == "" ? "?State=" + State : _condition + "&State=" + State;
    }
    if (UserID != 0) {
        _condition = _condition == "" ? "?UserID=" + UserID : _condition + "&UserID=" + UserID;
    }

    _url = _url + _condition;
    $("#POListGrid").jqGrid({
        url: _url,
        mtype: "Get",
        styleUI: "Bootstrap",
        datatype: "json",
        colModel: [
            { label: "", name: "ID", hidden: true },
            { label: '单号', name: 'PRNumber', width: 75 },
            { label: '供应商', name: "SupplierName", width: 75 },
            { label: '发起人', name: 'PRUser', width: 75 },
            { label: '发起日期', name: 'CreateDate', width: 75 },
            { label: '状态', name: 'Status', width: 75 },
        ],
        viewrecords: true,
        height: document.documentElement.clientHeight - 200,
        width: document.body.clientWidth * 0.875,
        rowNum: 500,
        loadonce: true,
        ondblClickRow: function (iRow) {

            location.href = "/Warehouse/POContentList?PurchaseOrderID=" + GetCurrentID("POListGrid");
        }
    })
}


//采购订单列表视图
function POListGrid(MoldNumber, Keyword, StartDate, EndDate, Supplier, State, PurchaseType) {
    var _url = "/Purchase/JsonPurchaseOrderList?State=" + State + "&";
    var _condition = "";
    if (MoldNumber != "") {
        _condition = "MoldNumber=" + MoldNumber;
    }

    if (Keyword != "") {
        _condition = _condition == "" ? "Keyword=" + Keyword : _condition + "&Keyword=" + Keyword;
    }

    if (StartDate != "") {
        _condition = _condition == "" ? "StartDate=" + StartDate : _condition + "&StartDate=" + StartDate;
    }

    if (EndDate != "") {
        _condition = _condition == "" ? "EndDate=" + EndDate : _condition + EndDate;
    }

    if (Supplier != 0) {
        _condition = _condition == "" ? "Supplier=" + Supplier : _condition + "&Supplier=" + Supplier;
    }

    if (PurchaseType != 0) {
        _condition = _condition == "" ? "PurchaseType=" + PurchaseType : _condition + "&PurchaseType=" + PurchaseType;
    }


    _url = _url + _condition;
    $("#POListGrid").jqGrid({
        url: _url,
        mtype: "GET",
        styleUI: 'Bootstrap',
        datatype: "json",
        colModel: [
            { label: "", name: "ID", hidden: true },
            { label: "订单号", name: "PONumber", width: 75 },
            { label: "供应商", name: "Supplier", width: 75 },
            { label: "总价", name: "TotalPrice", width: 75 },
            { label: "交货日期", name: "DueDate", width: 75, hidden: true },
            { label: "状态", name: "State", width: 75 },
            { label: "备注", name: "Memo", width: 75 },
            { label: "采购类型", name: "PurchaseType", width: 75 },
            { label: "生成人员", name: "PurchaseUser", width: 60 },
            { label: "生成日期", name: "CreateDate", width: 75 },
        ],
        height: document.documentElement.clientHeight - 220,
        width: document.body.clientWidth * 0.95,
        loadonce: true,
        ondblClickRow: function () {
            var _id = GetCurrentID("POListGrid");
            location.href = "/Purchase/PODetail?PurchaseOrderID=" + _id;
        }
    })
}

//订单内容列表
function POContentGrid(POID) {
    var _url = "/Purchase/JosnPOContents?PurchaseOrderID=" + POID;

    $("#POContentGrid").jqGrid({
        url: _url,
        mtype: "GET",
        styleUI: 'Bootstrap',
        datatype: "json",
        colModel: [
            { label: "", name: "POContentID", hidden: true },
            { label: "名称", name: "Name", width: 100 },
            { label: '物料编号', name: 'PartNumber', width: 75 },
            { label: '规格', name: 'Specification', width: 75 },
            { label: '数量', name: 'Quantity', width: 40 },
            { label: '单价（含税）', name: 'UnitPrice', width: 75 },
            { label: '金额（含税）', name: 'SubTotal', width: 75 },
            { label: '请购单号', name: 'PRNubmer', width: 50 },
            { label: '预计到货日期', name: "RequestTime", width: 50 },
            //{ lable: '备注', name: "Memo", width: 75 }
        ],
        viewrecords: true,
        height: document.documentElement.clientHeight - 200,
        width: document.body.clientWidth * 0.875,
        rowNum: 500,
        multiselect: true,
        loadonce: true,
        //ondblClickRow: function (iRow) {
        //    location.href = "/Purchase/PODetail?PurchaseOrderID=" + GetCurrentID("POListGrid");
        //}
    })
}


//仓库收货订单内容列表
function WarehouseAcceptGrid(POID) {
    var _url = "/Warehouse/JsonPOContents?POID=" + POID;

    $("#POContentGrid").jqGrid({
        url: _url,
        mtype: "GET",
        styleUI: 'Bootstrap',
        datatype: "json",
        colModel: [
            { label: "", name: "PurchaseItemID", hidden: true },
            { label: '零件名', name: 'Name', width: 75 },
            { label: '零件号', name: 'PartNumber', width: 75 },
            { label: '规格', name: 'Specification', width: 75 },
            { label: '应收数量', name: 'Quantity', width: 60 },
            { label: '到货数量', name: 'InStockQty', width: 60 },
            { label: '预计到货日期', name: 'PlanTime', width: 100, formatter: 'date' },
            { label: '备注', name: 'Memo', width: 75 },
        ],
        viewrecords: true,
        height: document.documentElement.clientHeight - 200,
        width: document.body.clientWidth * 0.875,
        rowNum: 500,
        multiselect: true,
        loadonce: true,
        ondblClickRow: function () {
            var _id = GetDblClickCell("POContentGrid", "PurchaseItemID");
            ShowPOContentToStockDlg(_id);
        }
    })
}

//询价单列表
function QRListGrid(ProjectID, State, UserID) {
    var _url = "/Purchase/JsonQRList";
    var _condition = "";
    if (ProjectID != 0) {
        _condition = _condition == "" ? "?ProjectID=" + ProjectID : _condition + "&ProjectID=" + ProjectID;
    }
    if (State != 0) {
        _condition = _condition == "" ? "?State=" + State : _condition + "&State=" + State;
    }

    if (UserID != 0) {
        _condition = _condition == "" ? "?UserID=" + UserID : _condition + "&UserID=" + UserID;
    }
    _url = _url + _condition;
    $("#QRListGrid").jqGrid({
        url: _url,
        mtype: "GET",
        styleUI: 'Bootstrap',
        datatype: "json",
        colModel: [
            { label: "", name: "QuotationRequestID", hidden: true },
            { label: '询价单号', name: 'QuotationNumber', width: 75 },
            { label: '采购员', name: 'PurchaseUser', width: 75 },
            { label: '生成日期', name: 'CreateDate', width: 100 },
            { label: '已选供应商', name: 'Supplier', width: 100 },
            { label: '已报价供应商', name: 'Supplier', width: 100 },
            { label: '报价需求日期', name: "DueDate", width: 100 },
            { label: '状态', name: 'State', width: 75 },

        ],
        viewrecords: true,
        height: document.documentElement.clientHeight - 240,
        width: document.body.clientWidth * 0.95,
        rowNum: 500,
        loadonce: true,
        multiselect: true,
        ondblClickRow: function () {

            var _id = GetDblClickCell("QRListGrid", "QuotationRequestID");
            location.href = "/Purchase/QRDetail?QuotationRequestID=" + _id;
        }
    })
}

//询价单内容列表
function QRContentGrid(PRContentIDs, QRID, state, PurchaseItemIDs) {
    var _url;
    if (PRContentIDs != "") {
        _url = "/Purchase/JsonQRNew?PRContentIDs=" + PRContentIDs;
    } else if (QRID != 0) {
        _url = "/Purchase/JsonQRDetail?QRID=" + QRID;
    } else if (PurchaseItemIDs != "") {
        _url = "/Purchase/JsonQRPurchaseItems?PurchaseItemIDs=" + PurchaseItemIDs;
    } else {
        _url = "";
    }
    $("#QRContentGrid").jqGrid({
        url: _url,
        mtype: "GET",
        styleUI: 'Bootstrap',
        datatype: "json",
        colModel: [
            { label: "", name: "QRContentID", hidden: true },
            { label: '零件名', name: 'PartName', width: 100 },
            { label: '零件号', name: 'PartNumber', width: 75 },
            { label: '数量', name: 'Quantity', width: 30 },
            { label: '规格', name: 'PartSpecification', width: 75 },
            { label: '材料', name: 'MaterialName', width: 100 },
            { label: '硬度', name: 'Hardness', width: 75 },
            { label: '品牌', name: 'BrandName', width: 75 },
            { label: '附图', name: 'PurchaseDrawing', formatter: 'checkbox', width: 75 },
            { label: '', name: 'PRContentID', hidden: true },
            { label: '', name: "PurchaseItemID", hidden: true },
            { label: '需求日期', name: 'RequireDate', width: 75 },
        ],
        viewrecords: true,
        height: document.documentElement.clientHeight - 200,
        width: document.body.clientWidth * 0.875,
        rowNum: 500,
        loadonce: true,
        multiselect: true,
        ondblClickRow: function (iRow) {
            if (dept == 4) {
                ModifyQRContentQty(GetDblClickCell("QRContentGrid", "QRContentID"));
            }
        }
    })
}


function SupplierGrid(Mode) {
    var _url = "/Purchase/JsonSuppliers";
    $("#SupplierGrid").jqGrid({
        url: _url,
        mtype: "GET",
        styleUI: "Bootstrap",
        datatype: "json",
        colModel: [
            { label: '', name: 'SupplierID', hidden: true },
            { label: '代码', name: 'Code', width: 50 },
            { label: '名称', name: 'Name', width: 50 },
            { label: '全名', name: 'FullName', width: 100 },
            { label: '地址', name: 'Address', width: 100 },
            { label: '开户行', name: 'Bank', width: 75, hidden: true },
            { label: '账号', name: 'Account', width: 75, hidden: true },
            { label: '税号', name: 'TaxNo', width: 75, hidden: true },
            { label: '税率', name: 'TaxRate', width: 30, hidden: true },
            { label: '结算方式', name: 'Settlement', width: 50, hidden: true }
        ],
        viewrecords: true,
        height: document.documentElement.clientHeight - 250,
        width: document.body.clientWidth * 0.875,
        multiselect: true,
        rowNum: 500,
        loadonce: true,
        ondblClickRow: function (iRow) {
            if (Mode == 0) {
                var id = $("#SupplierGrid").getCell(iRow, "SupplierID");
                LoadSupplier(id);
            }

        },
        onCellSelect: function (iRow) {
            var id = $("#SupplierGrid").getCell(iRow, "SupplierID");
            $("#SupplierContactGrid").jqGrid('setGridParam', { url: '/Purchase/JsonContacts?SupplierID=' + id }).trigger("reloadGrid");
        }

    })
}

function TaskFinish(TaskType) {
    var _url = "/Task/JsonFinishTasks?TaskType=" + TaskType;
    $("#TaskGrid").jqGrid({
        url: _url,
        mtype: "GET",
        styleUI: "Bootstrap",
        datatype: "json",
        colModel: [
            { label: '', name: 'ID', hidden: true },
            { label: '名称', name: 'TaskName', width: 150 },
            { label: '开始时间', name: 'StartTime', width: 100 },
            { label: '机床', name: 'Machine', width: 100 }
        ],
        viewrecords: true,
        height: document.documentElement.clientHeight - 250,
        width: document.body.clientWidth * 0.92,
        multiselect: true,
        rowNum: 500,
        loadonce: true,

    })
}

function TaskRecreateGrid(ProjectID, TaskType, Multi) {
    var _height;
    var _width;
    var _url;
    var _multi;
    switch (TaskType) {
        case 1:
            _height = (document.documentElement.clientHeight - 220) * 0.55;
            _width = document.body.clientWidth * 0.55;
            break;
        case 2:
            _height = (document.documentElement.clientHeight - 220) * 0.385;
            _width = document.body.clientWidth * 0.6;
            break;
        case 4:
            _height = (document.documentElement.clientHeight - 220) * 0.55;
            _width = document.body.clientWidth * 0.55;
            break;
    }

    if (Multi == undefined) {
        _multi = false;
    } else {
        _multi = true;
    }

    if (ProjectID == 0) {
        _url = "";
    } else {
        _url = '/Task/JsonMachineTasks?ProjectID=' + ProjectID + "&TaskType=" + TaskType + "&State=0";
    }

    $("#TaskGrid").jqGrid({
        url: _url,
        mtype: "GET",
        styleUI: 'Bootstrap',
        datatype: "json",
        colModel: [
            { label: "", name: "ID", hidden: true },
            { label: '图纸', name: 'DrawingFile', width: 40 },
            { label: '任务名', name: 'TaskName', width: 100 },
            { label: '版本', name: 'Version', width: 40 },
            { label: '工艺名', name: 'ProcessName', width: 100, hidden: true },
            { label: '型号', name: 'Model', width: 75, hidden: true },
            { label: 'R', name: 'R', width: 30, hidden: true },
            { label: 'F', name: 'F', width: 30, hidden: true },
            { label: 'HRC', name: 'HRC', width: 75, hidden: true },
            { label: '材料', name: 'Material', width: 75, hidden: true },
            { label: '时间', name: 'Time', width: 40, hidden: true },
            { label: '状态', name: 'State', width: 40, hidden: true },
            { label: '毛坯', name: 'Raw', width: 75, hidden: true },
            { label: '备料', name: 'Prepared', formatter: "checkbox", width: 40, hidden: true },
            { label: '优先级', name: 'Priority', width: 40, hidden: true },
            { label: '数量', name: 'Quantity', width: 30, hidden: true },
            { label: '创建时间', name: 'CreateTime', width: 75 },
            { label: '预计时间', name: 'ForecastTime', width: 75, hidden: true },
            { label: '接收时间', name: 'AcceptTime', width: 75, hidden: true },
            { label: '发布时间', name: 'ReleaseTime', width: 75, hidden: true },
            { label: '计划时间', name: 'PlanTime', width: 75, hidden: true },
            { label: '开始时间', name: 'StartTime', width: 75, hidden: true },
            { label: '备注', name: 'Memo', width: 75 },
            { label: '状态备注', name: 'StateMemo', width: 75, hidden: true },
            { label: 'CAD', name: 'CAD', width: 75, hidden: true },
            { label: 'CAM', name: 'CAM', width: 75, hidden: true },
            { label: '加工', name: 'Workshop', width: 75, hidden: true },
            { label: 'QC', name: 'QC', width: 75, hidden: true },
        ],
        viewrecords: true,
        height: _height, //document.documentElement.clientHeight - 220,
        width: _width, //document.body.clientWidth * 0.875,
        rowNum: 50,
        multiselect: _multi,
        loadonce: true,
        onSelectRow: function (id) {
            //alert(id);
            if (TaskType == 2) {
                LoadEleDetail(GetCurrentID("TaskGrid"));
                LoadCADDetail(GetCurrentID("TaskGrid"));
            }
            if ((TaskType == 1) || (TaskType == 4)) {
                var _id = GetCurrentID("TaskGrid")

                LoadEleDetail(_id);
            }
        }
    });
}

function ElectrodeQCResult(Status, Keyword) {

    var _url = "/Task/JsonEleInfo?Status=" + Status;
    if ((Keyword != "") & (Keyword != undefined)) {
        _url = _url + "&Keyword=" + Keyword;
    }
    $("#ElectrodeGrid").jqGrid({

        url: _url,
        mtype: "GET",
        styleUI: "Bootstrap",
        datatype: "Json",
        colModel: [
            { label: '', name: 'ID', hidden: true },
            { label: "电极标签名", name: "LabelName", width: 150 },
            { label: "火花间隙补偿", name: "GapCompensation", width: 50 },
            { label: "高度补偿", name: "ZCompensation", width: 50 },
            { label: "电极名", name: "EleName", width: 150 },
            { label: "QC完成时间", name: "QCFinsihTime", width: 75 },
            { label: "火花间隙", name: "Gap", width: 50 }
        ],
        viewrecords: true,
        height: document.documentElement.clientHeight * 0.75,
        width: document.body.clientWidth * 0.92,
        //multiselect: true,
        rowNum: 500,
        loadonce: true,
        ondblClickRow: function (iRow) {
            //var id = $("#ElectrodeGrid").getCell(iRow, "ID");
            LoadElectrode();
        },
    })
}


function ElectrodeInStockRecord(MoldNumber) {
    var _url = "";
    if (MoldNumber != "") {
        _url = "/Task/JsonEleInStock?MoldNumber=" + MoldNumber;
    }

    $("#ElectrodeGrid").jqGrid({

        url: _url,
        mtype: "GET",
        styleUI: "Bootstrap",
        datatype: "Json",
        colModel: [
            { label: '', name: 'ID', hidden: true },
            { label: "电极标签名", name: "LabelName", width: 150 },
            { label: "材料", name: "Material", width: 50 },
            { label: "创建时间", name: "CreateTime", width: 50 },
            { label: "状态", name: "Status", width: 50 }
        ],
        viewrecords: true,
        height: document.documentElement.clientHeight * 0.72,
        width: document.body.clientWidth * 0.75,
        multiselect: true,
        rowNum: 500,
        loadonce: true,
        ondblClickRow: function (iRow) {
            //var id = $("#ElectrodeGrid").getCell(iRow, "ID");
            LoadElectrode();
        },
    })
}

function WHRequestGrid() {
    var _url = "/Warehouse/JsonWarehouseRequests?Keyword=&StartDate=&EndDate=&RequestKeyword=";

    $("#WHRequestGrid").jqGrid({
        url: _url,
        mtype: "GET",
        styleUI: "Bootstrap",
        datatype: "Json",
        colModel: [
            { label: "", name: "ID", hidden: true },
            { label: "申请单号", name: "RequestNumber", width: 150 },
            { label: "申请人", name: "RequestUser", width: 100 },
            { label: "状态", name: "State", width: 50 },
            { label: "申请日期", name: "RequestDate", width: 100 },
        ],
        viewrecords: true,
        height: document.documentElement.clientHeight * 0.72,
        width: document.body.clientWidth * 0.75,
        loadonce: true,
        ondblClickRow: function (iRow) {
            //var id = $("#ElectrodeGrid").getCell(iRow, "ID");
            LoadWHRequest(GetCurrentID("WHRequestGrid"));
        }
    })
}


function WHRequestItemGrid(WHRequestID, Mode) {
    var _url = "";
    if (WHRequestID != 0) {
        _url = "/Warehouse/JsonWarehouseRequestItems?WarehouseRequestID=" + WHRequestID;
    }
    var _width;
    switch (Mode) {
        case 1:
            _width = document.body.clientWidth * 0.45;
            break;
        default:
            _width = document.body.clientWidth * 0.85;
    }
    var lastsel2;
    $("#WHRequestItemGrid").jqGrid({
        url: _url,
        mtype: "GET",
        styleUI: "Bootstrap",
        datatype: "Json",
        colModel: [
            { label: "", name: "ID", hidden: true },
            { label: "零件名", name: "PartName", width: 120 },
            { label: "物料号", name: "PartNumber", width: 100 },
            { label: "规格", name: "Specification", width: 100 },
            { label: "库存", name: "InStockQuantity", width: 40, hidden: true },
            {
                label: "领用数量", name: "Quantity", width: 60, editable: true,
                editrules: {
                    custom: true, number: true, custom_func: function (value, rowid) {
                        var _col = $("#WHRequestItemGrid").getGridParam("selrow")

                        var total = Number($("#WHRequestItemGrid").getCell(_col, "InStockQuantity"));
                        if (value > total) {
                            return [false, "领用数量不能大于库存数量"];
                        } else {


                            if (Number(value) <= 0) {
                                return [false, "领用数量必须大于0"];
                            }


                            var _id = Number($("#WHRequestItemGrid").getGridParam("selrow"));

                            $("#WHRequestItemGrid").setSelection(_id);
                            $("#WHRequestItemGrid").setSelection(_id + 1);
                            $('#WHRequestItemGrid').jqGrid('editRow', _id + 1, true);
                            //alert();
                            return [true];
                        }
                    }
                }

            },
            { label: "已领数量", name: "ReceivedQuantity", width: 50, hidden: true }
        ],
        viewrecords: true,
        height: document.documentElement.clientHeight - 220,
        width: _width,
        multiselect: true,
        loadonce: true,
        editurl: "",
        cellsubmit: "clientArray",
        ondblClickRow: function (id) {
            $('#WHRequestItemGrid').jqGrid('setSelection', id);
            if (WHRequestID == 0) {
                if (id != lastsel2) {
                    $('#WHRequestItemGrid').jqGrid('restoreRow', lastsel2);

                    lastsel2 = id;
                }
                $('#WHRequestItemGrid').jqGrid('editRow', id, true);

            }
            $(".inline-edit-cell").on("focus", function () {
                var _id = this.id;
                $("#" + _id).attr("type", "number");
                $("#" + _id).attr("min", "1");
                this.select();
            })
        },
        onSelectRow: function (id) {
            var _request = Number($("#WHRequestItemGrid").getCell(id, "Quantity"));
            var _receive = Number($("#WHRequestItemGrid").getCell(id, "ReceivedQuantity"));
            if (_request <= _receive) {
                $("#WHRequestItemGrid").jqGrid("setSelection", id, false);
            }
        },
        onSelectAll: function (rowid) { //点击全选时触发事件
            //var _rowids = rowid;
            //var _totalrows = rowid.length;


            var rowIds = jQuery("#WHRequestItemGrid").jqGrid('getDataIDs');//获取jqgrid中所有数据行的id
            for (i = 0; i < rowIds.length ; i++) {
                var _request = Number($("#WHRequestItemGrid").getCell(rowIds[i], "Quantity"));
                var _receive = Number($("#WHRequestItemGrid").getCell(rowIds[i], "ReceivedQuantity"));
                if (_request <= _receive) {

                    $("#WHRequestItemGrid").jqGrid("setSelection", rowIds[i], false); //设置改行不能被选中。                    
                }
            }
        }
    })
}

function PRContentGrid_T() {
    var _url = "";
    $("#PRContentGrid").jqGrid({
        url: _url,
        mtype: "GET",
        styleUI: 'Bootstrap',
        datatype: "json",
        colModel: [
            { label: "", name: "ID", hidden: true },
            { label: "", name: "PartID", hidden: true },
            { label: "", name: "TaskID", hidden: true },
            { label: "零件名", name: "Name", width: 100 },
            { label: "物料号", name: "PartNumber", width: 100 },
            { label: "备注", name: "Memo", width: 100 },
            { label: "供应商", name: "SupplierNmae", width: 100 },
            { label: "采购类型", name: "PurchaseType", width: 100 },
            { label: "计划时间", name: "PlanTime", width: 100 },
            { label: "预计时间", name: "ForecasetTime", width: 100 },
            { label: "结束时间", name: "FinishTime", width: 100 },
            { label: "生成时间", name: "CreateTime", width: 100 },
            { label: "生成人员", name: "Creator", width: 60 },
            { label: "状态", name: "State", width: 100 },
            { label: "到库数量", name: "InStockQty", width: 40 },
            { label: "领出数量", name: "OutStockQty", width: 40 },
            { label: "单价", name: "UnitPrice", width: 60 },
            { label: "总价", name: "TotalPrice", width: 60 },
            { label: "材料", name: "Material", width: 100 },
            { label: "尺寸", name: "Specification", width: 100 },
            { label: "数量", name: "Quantity", width: 100 },
            { label: "附图加工", name: "PurchaseWithDrawing", width: 100 },
        ],
        viewrecords: true,
        height: document.documentElement.clientHeight - 300,
        //width: document.body.clientWidth * 0.875,
        rowNum: 500,
        multiselect: true,
        loadonce: true,
        ondblClickRow: function (iRow) {

            if (state < 2) {
                var row = $(event.target).closest("tr.jqgrow").attr("id");
                var _id = $("#PRContentGrid").getCell(row, "ID");
                EditPrContent(_id, row);
            }
        }
    })
}


function OutSourceGrid_T() {
    var _url = "";
    $("#OutSourceGrid").jqGrid({
        url: _url,
        mtype: "GET",
        styleUI: 'Bootstrap',
        datatype: "json",
        colModel: [
            { label: "", name: "ID", hidden: true },
            { label: "零件名", name: "Name", width: 100 },
            { label: "物料号", name: "PartNumber", width: 100 },
            { label: "型号", name: "Memo", width: 100 },
            { label: "数量", name: "SupplierNmae", width: 100 },
            { label: "计划时间", name: "PurchaseType", width: 100 },
            { label: "生成人员", name: "PlanTime", width: 100 },
            { label: "生成时间", name: "ForecasetTime", width: 100 },
        ],
        viewrecords: true,
        height: document.documentElement.clientHeight - 200,
        width: document.body.clientWidth * 0.75,
        rowNum: 500,
        multiselect: true,
        loadonce: true,
        ondblClickRow: function (iRow) {

            if (state < 2) {
                var row = $(event.target).closest("tr.jqgrow").attr("id");
                var _id = $("#PRContentGrid").getCell(row, "ID");
                EditPrContent(_id, row);
            }
        }
    })
}

function WHContentGrid_T() {
    var _url = "";
    $("#OutSourceGrid").jqGrid({
        url: _url,
        mtype: "GET",
        styleUI: 'Bootstrap',
        datatype: "json",
        colModel: [
            { label: "", name: "ID", hidden: true },
            { label: "零件名", name: "Name", width: 100 },
            { label: "物料号", name: "PartNumber", width: 100 },
            { label: "品名规格", name: "Memo", width: 100 },
            { label: "材料", name: "SupplierNmae", width: 100 },
            { label: "收到数量", name: "PurchaseType", width: 100 },
            { label: "到达日期", name: "PlanTime", width: 100 },
            { label: "库位", name: "ForecasetTime", width: 100 },
            { label: "供应商", name: "ForecasetTime", width: 100 }
        ],
        viewrecords: true,
        height: document.documentElement.clientHeight - 200,
        width: document.body.clientWidth * 0.75,
        rowNum: 500,
        multiselect: true,
        loadonce: true,
        ondblClickRow: function (iRow) {

            if (state < 2) {
                var row = $(event.target).closest("tr.jqgrow").attr("id");
                var _id = $("#PRContentGrid").getCell(row, "ID");
                EditPrContent(_id, row);
            }
        }
    })
}


function POContentGrid_T() {
    var _url = "";
    $("#POContentGrid").jqGrid({
        url: _url,
        mtype: "GET",
        styleUI: 'Bootstrap',
        datatype: "json",
        colModel: [
            { label: "", name: "ID", hidden: true },
            { label: "零件名", name: "Name", width: 100 },
            { label: "物料号", name: "PartNumber", width: 100 },
            { label: "备注", name: "Memo", width: 100 },
            { label: "计划时间", name: "SupplierNmae", width: 100 },
            { label: "预计时间", name: "PurchaseType", width: 100 },
            { label: "生成时间", name: "PlanTime", width: 100 },
            { label: "生成人员", name: "ForecasetTime", width: 100 },
            { label: "状态", name: "ForecasetTime", width: 100 },
            { label: "入库数量", name: "ForecasetTime", width: 100 },
            { label: "PO数量", name: "ForecasetTime", width: 100 },
            { label: "尺寸", name: "ForecasetTime", width: 100 },
            { label: "订单号", name: "ForecasetTime", width: 100 }
        ],
        viewrecords: true,
        height: document.documentElement.clientHeight - 200,
        width: document.body.clientWidth * 0.75,
        rowNum: 500,
        multiselect: true,
        loadonce: true,
        ondblClickRow: function (iRow) {

            if (state < 2) {
                var row = $(event.target).closest("tr.jqgrow").attr("id");
                var _id = $("#PRContentGrid").getCell(row, "ID");
                EditPrContent(_id, row);
            }
        }
    })
}


function OutStockRecordGrid_T() {
    var _url = "";
    $("#POContentGrid").jqGrid({
        url: _url,
        mtype: "GET",
        styleUI: 'Bootstrap',
        datatype: "json",
        colModel: [
            { label: "", name: "ID", hidden: true },
            { label: "零件名", name: "Name", width: 100 },
            { label: "物料号", name: "PartNumber", width: 100 },
            { label: "状态", name: "PartNumber", width: 100 },
            { label: "备注", name: "Memo", width: 100 },
            { label: "供应商", name: "SupplierNmae", width: 100 },
            { label: "材料", name: "PurchaseType", width: 100 },
            { label: "数量", name: "PlanTime", width: 100 },
            { label: "价格", name: "ForecasetTime", width: 100 },
            { label: "计划时间", name: "ForecasetTime", width: 100 },
            { label: "类型", name: "ForecasetTime", width: 100 },
            { label: "结束时间", name: "ForecasetTime", width: 100 },
            { label: "生成时间", name: "ForecasetTime", width: 100 },
            { label: "生成人员", name: "ForecasetTime", width: 100 },
            { label: "出库单编号", name: "ForecasetTime", width: 100 }
        ],
        viewrecords: true,
        height: document.documentElement.clientHeight - 200,
        width: document.body.clientWidth * 0.75,
        rowNum: 500,
        multiselect: true,
        loadonce: true,
        ondblClickRow: function (iRow) {

            if (state < 2) {
                var row = $(event.target).closest("tr.jqgrow").attr("id");
                var _id = $("#PRContentGrid").getCell(row, "ID");
                EditPrContent(_id, row);
            }
        }
    })
}


function PRContent_PO_T(Keyword, PurchaseType) {
    var _url = "";
    $("#PRContentGrid").jqGrid({
        url: _url,
        mtype: "GET",
        styleUI: 'Bootstrap',
        datatype: "json",
        colModel: [
            { label: "", name: "ID", hidden: true },
            { label: "零件名", name: "Name", width: 120 },
            { label: "物料号", name: "PartNumber", width: 70 },
            { label: "规格", name: "Specification", width: 70 },
            { label: "数量", name: "Quantity", width: 30, hidden: true },
            { label: "状态", name: "State", width: 40, sortable: true },
            { label: "采购类型", name: "PurchaseType", width: 60 },
            { label: "申请单号", name: "PurchaseRequest", width: 55, hidden: true },
            { label: "询价单号", name: "QuotationRequest", width: 55, hidden: true },
            { label: "订单号", name: "PurchaseOrder", width: 55, hidden: true },
            { label: "供应商", name: "Supplier", width: 60 },
        ],
        viewrecords: true,
        height: document.documentElement.clientHeight - 200,
        width: document.body.clientWidth * 0.47,
        rowNum: 500,
        multiselect: true,
        loadonce: true,
        ondblClickRow: function (iRow) {

            if (state < 2) {
                var row = $(event.target).closest("tr.jqgrow").attr("id");
                var _id = $("#PRContentGrid").getCell(row, "ID");
                EditPrContent(_id, row);
            }
        },
        loadComplete: function () {
            $(".jqgrow", this).contextMenu("PurchaseItemContextMenu", {
                bindings: {
                    //Go to the create new mold project page
                    'AddItem': function () {
                        var _itemIDs = GetMultiSelectedIDs("PRContentGrid");
                        console.log(_itemIDs);
                        AddPOItem(_itemIDs);
                    },
                },
                onContextMenu: function (event/*, menu*/) {
                    $("#ProjectGrid").jqGrid("setSelection", $(event.target).closest("tr.jqgrow").attr("id"));
                    var item = $(event.target).closest("td");
                    $("#selPhase").val(item[0].id);


                    return true;
                }
            });
        }
    })
}

function POContent_PO_T(ItemIDs) {
    if (ItemIDs == "") {
        var _url = "";
    } else {
        var _url = "/Purchase/AddPOItem?ItemIDs=" + ItemIDs;
    }

    var lastsel2;
    var curRow;
    $("#POContentGrid").jqGrid({
        url: _url,
        mtype: "GET",
        styleUI: 'Bootstrap',
        datatype: "json",
        colModel: [
            { label: "", name: "PurchaseItemID", hidden: true },
            { label: "零件名", name: "Name", width: 120 },
            { label: "规格", name: "Specification", width: 70 },
            { label: "数量", name: "Quantity", width: 60, editable: true, },
            { label: "单价(含税）", name: "UnitPrice", width: 60, editable: true, },
            {
                label: "总价(含税）", name: "TotalPrice", width: 60, editable: true,
                editrules: {
                    custom: true, custom_func: function () {
                        curRow = Number(curRow) + 1;
                        EditNextRow(curRow);
                        return [true];
                    }
                }
            },
            { label: "交货日期", name: "DeliverDate", width: 70 },
            { label: "备注", name: "Memo", width: 60 },

        ],
        viewrecords: true,
        height: document.documentElement.clientHeight - 200,
        width: document.body.clientWidth * 0.47,
        rowNum: 500,
        multiselect: true,
        loadonce: true,
        editurl: "",
        cellsubmit: "clientArray",
        loadComplete: function () {
            $(".jqgrow", this).contextMenu("POItemContextMenu", {
                bindings: {
                    //Go to the create new mold project page
                    'RemoveItem': function () {
                        RemovePOItem();
                    },
                    'DeliveryDate': function () {
                        ShowSetDeliverDate();
                    },
                    'BatchPriceShow': function () {
                        ShowBatchPrice();
                    },
                    'HistoryPrice': function () {
                        ShowHistory();
                    }


                },
                onContextMenu: function (event/*, menu*/) {
                    $("#ProjectGrid").jqGrid("setSelection", $(event.target).closest("tr.jqgrow").attr("id"));
                    var item = $(event.target).closest("td");
                    $("#selPhase").val(item[0].id);
                    return true;
                }
            });

        },
        onCellSelect: function (rowid, iCol, cellcontent, e) {
            //alert("rowid=" + rowid + " iCol=" + iCol + " cellContent=" + cellcontent);
            for (i = 1; i <= $("#POContentGrid").jqGrid("getDataIDs").length ; i++) {
                $('#POContentGrid').jqGrid('saveRow', i);
            }
            $('#POContentGrid').jqGrid('editRow', rowid, true);
            curRow = rowid;
            BindRowAction(rowid, iCol);

        }
    })
}




function CheckFieldNotZero(curRow) {
    var fields = $(".inline-edit-cell[name^='curRow'");

    for (i = 0; i < fields.length; i++) {
        if ((fields[i].value == 0) || (isNaN(Number(fields[i].value)))) {
            fields[i].select();
            return false
        }
    }
    curRow = Number(curRow) + 1
    $('#POContentGrid').jqGrid('editRow', curRow, true);
    BindRowAction(curRow);
    return true;
}

function EditNextRow(curRow) {
    $('#POContentGrid').jqGrid('editRow', curRow, true);
    BindRowAction(curRow);
}



function PurchaseItem(Keyword, MoldNumber, State, PurchaseType) {
    var _url = "/Purchase/JsonPurchaseItems";
    var _condition = "";
    if (Keyword != "") {
        _condition = "?Keyword=" + Keyword;
    }
    if (MoldNumber != "") {
        _condition = _condition == "" ? "?MoldNumber=" + MoldNumber : _condition + "&MoldNumber=" + MoldNumber;
    }

    if (State != undefined) {
        _condition = _condition == "" ? "?State=" + State : _condition + "&State=" + State;
    }

    if (!isNaN(Number(PurchaseType))) {
        _condition = _condition == "" ? "?PurchaseType=" + PurchaseType : _condition + "&PurchaseType=" + PurchaseType;
    }

    _url = _url + _condition;
    $("#PurchaseItemGrid").jqGrid({
        url: _url,
        mtype: "GET",
        styleUI: 'Bootstrap',
        datatype: "json",
        colModel: [
            { label: "", name: "ID", hidden: true },
            { label: "零件名", name: "Name", width: 100 },
            { label: "物料号", name: "PartNumber", width: 60 },
            { label: "规格", name: "Specification", width: 100 },
            { label: "订单数量", name: "Quantity", width: 50 },
            { label: "状态", name: "State", width: 60 },
            { label: "采购类型", name: "PurchaseType", width: 60 },
            { label: "申请单号", name: "PurchaseRequest", width: 55 },
            { label: "询价单号", name: "QuotationRequest", width: 55, hidden: true },
            { label: "订单号", name: "PurchaseOrder", width: 55, hidden: true },
            { label: "供应商", name: "Supplier", width: 60, hidden: true },
            { label: "采购人员", name: 'PurchaseUser', width: 60, hidden: true },
            { label: "单价", name: "UnitPrice", width: 100, hidden: true },
            { label: "总价", name: "TotalPrice", width: 60, hidden: true, sorttype: 'integer' },
            { label: "到库数", name: "InStockQty", width: 40, hidden: true },
            { label: "领用数", name: "OutStockQty", width: 40, hidden: true },
            { label: "生成人员", name: "RequestUser", width: 40 },
            { label: "总价", name: "TotalPriceWT", width: 40, hidden: true, sorttype: 'integer' },
            { label: "需求日期", name: "ShipDate", width: 60, hidden: true },
            { label: "计划到货日期", name: "PlanDate", width: 60, hidden: true },
            { label: "到货日期", name: "DeliveryDate", width: 60, hidden: true },
        ],
        viewrecords: true,
        height: document.documentElement.clientHeight - 200,
        width: document.body.clientWidth * 0.8,
        rowNum: 500,
        loadonce: true,
        multiselect: true,
        ondblClickRow: function (iRow) {

            //if (state < 2) {
            //    var row = $(event.target).closest("tr.jqgrow").attr("id");
            //    var _id = $("#PRContentGrid").getCell(row, "ID");
            //    EditPrContent(_id, row);
            //}
        }
    })
}

function OutSourceItem() {
    var _url = "";//"/Purchase/JsonOutSourceItems?PurchaseOrderID="+PurchaseOrderID;

    $("#PurchaseItemGrid").jqGrid({
        url: _url,
        mtype: "GET",
        styleUI: 'Bootstrap',
        datatype: "json",
        colModel: [
            { label: "", name: "ID", hidden: true },
            { label: "零件名", name: "Name", width: 100 },
            { label: "物料号", name: "PartNumber", width: 60 },
            { label: "规格", name: "Specification", width: 100 },
            { label: "数量", name: "Quantity", width: 30 },
            { label: "状态", name: "State", width: 60 },
            { label: "采购类型", name: "PurchaseType", width: 60 },
            { label: "申请单号", name: "PurchaseRequest", width: 55, hidden: true },
            { label: "询价单号", name: "QuotationRequest", width: 55, hidden: true },
            { label: "订单号", name: "PurchaseOrder", width: 55 },
            { label: "供应商", name: "Supplier", width: 60, },
            { label: "预计交期", name: "ShipDate", width: 60, hidden: true },
            { label: "采购人员", name: 'PurchaseUser', width: 60, hidden: true },
            { label: "单价", name: "UnitPrice", width: 100, hidden: true },
            { label: "总价", name: "TotalPrice", width: 60, hidden: true },
            { label: "到库数", name: "InStockQty", width: 40, hidden: true },
            { label: "领用数", name: "OutStockQty", width: 40, hidden: true },
            { label: "生成人员", name: "RequestUser", width: 40 },
            { label: "生成时间", name: "CreateTime", width: 80 },
            { label: "计划时间", name: "PlanTime", width: 80 },
        ],
        viewrecords: true,
        height: document.documentElement.clientHeight - 210,
        width: document.body.clientWidth * 0.7,
        rowNum: 500,
        loadonce: true,
        multiselect: true,
        ondblClickRow: function (iRow) {

            //if (state < 2) {
            //    var row = $(event.target).closest("tr.jqgrow").attr("id");
            //    var _id = $("#PRContentGrid").getCell(row, "ID");
            //    EditPrContent(_id, row);
            //}
        }
    })
}


function OutStockHistory(MoldNumber, Keyword) {
    var _url = "/Warehouse/JsonOutStock";
    var _condition = "";
    if (Keyword != "") {
        _condition = "?Keyword=" + Keyword;
    }
    if (MoldNumber != "") {
        _condition = _condition == "" ? "?MoldNumber=" + MoldNumber : "&MoldNumber=" + MoldNumber;
    }
    _url = _url + _condition;

    $("#OutStockHistoryGrid").jqGrid({
        url: _url,
        mtype: "GET",
        styleUI: 'Bootstrap',
        datatype: "json",
        colModel: [
            { label: "", name: "ID", hidden: true },
            { label: "零件名", name: "PartName", width: 100 },
            { label: "物料号", name: "PartNumber", width: 60 },
            { label: "规格", name: "Specification", width: 100 },
            { label: "领用数量", name: "Quantity", width: 30 },
            { label: "领料人", name: "RequestUser", width: 60 },
            { label: "领用时间", name: "OutDate", width: 80 },
            { label: "发料人", name: "WarehouseUser", width: 60 },
        ],
        viewrecords: true,
        height: document.documentElement.clientHeight - 215,
        width: document.body.clientWidth * 0.7,
        rowNum: 500,
        loadonce: true,
    })
}


function ReturnRequestGrid(State) {
    var _url = "/Warehouse/JsonReturnRequests?State=" + State;
    var lastsel2;
    var curRow;
    $("#ReturnRequestGrid").jqGrid({
        url: _url,
        mtype: "Get",
        styleUI: 'Bootstrap',
        datatype: "json",
        colModel: [
                { label: "", name: "ID", hidden: true },
                { label: "退货单号", name: "Name", width: 100 },
                { label: "订单号", name: "PurchaseOrderNumber", width: 100 },
                { label: "供应商", name: "MaterialNumber", width: 60 },
                { label: "状态", name: "State", width: 60 },
                { label: "创建时间", name: "Specification", width: 60 },
                { label: "创建人", name: "PurchaseQuantity", width: 60 },
                { label: "审批时间", name: "InStockQuantity", width: 60 },
                { label: "审批人", name: "InStockQuantity", width: 60 },
                { label: "关闭时间", name: "ReturnDate", width: 60 },
        ],
        viewrecords: true,
        height: document.documentElement.clientHeight - 200,
        width: document.body.clientWidth * 0.9,
        rowNum: 100,
        loadonce: true,
        ondblClickRow: function (rowid) {
            var _id = $("#ReturnRequestGrid").getCell(rowid, 'ID');
            LoadReturnRequestDetail(_id);
        },
    })
}


function ReturnItemGrid(ReturnRequestID, PurchaseItemIDs) {
    var _url = "/Warehouse/JsonReturnItems?ReturnRequestID=" + ReturnRequestID + "&PurchaseItemIDs=" + PurchaseItemIDs;
    var lastsel2;
    var curRow;
    $("#ReturnItemGrid").jqGrid({
        url: _url,
        mtype: "Get",
        styleUI: 'Bootstrap',
        datatype: "json",
        colModel: [
                { label: "", name: "ID", hidden: true },
                { label: "", name: "PurchaseItemID", hidden: true },
                { label: "", name: "WarehouseStockID", hidden: true },

                { label: "零件名", name: "Name", width: 100 },
                { label: "物料号", name: "MaterialNumber", width: 60 },
                { label: "规格", name: "Specification", width: 100 },
                { label: "订单数量", name: "PurchaseQuantity", width: 60 },
                { label: "到货数量", name: "InStockQuantity", width: 60 },
                { label: "库存数量", name: "InStockQuantity", width: 60 },
                { label: "退货数量", name: "Quantity", width: 60, editable: true },
                { label: "退货原因", name: "Memo", width: 200, editable: true },
                { label: "", name: "Enabled", hidden: true }
        ],
        viewrecords: true,
        height: document.documentElement.clientHeight - 200,
        width: document.body.clientWidth * 0.9,
        rowNum: 100,
        multiselect: true,
        loadonce: true,
        onCellSelect: function (rowid, iCol, cellcontent, e) {
            if ($("#Editable").val() == "true") {
                for (i = 1; i <= $("#ReturnItemGrid").jqGrid("getDataIDs").length ; i++) {
                    $('#ReturnItemGrid').jqGrid('saveRow', i);
                }
                $('#ReturnItemGrid').jqGrid('editRow', rowid, true);
                curRow = rowid;
                BindRowAction(rowid, iCol)
            }

        }
    })
}











//合并单元格方法
function Merger(gridName, CellName) {
    //得到显示到界面的id集合
    var mya = $("#" + gridName + "").getDataIDs();
    //当前显示多少条
    var length = mya.length;
    for (var i = 0; i < length; i++) {
        //从上到下获取一条信息
        var before = $("#" + gridName + "").jqGrid('getRowData', mya[i]);
        //定义合并行数
        var rowSpanTaxCount = 1;
        for (j = i + 1; j <= length; j++) {
            //和上边的信息对比 如果值一样就合并行数+1 然后设置rowspan 让当前单元格隐藏
            var end = $("#" + gridName + "").jqGrid('getRowData', mya[j]);
            if (before[CellName] == end[CellName]) {
                rowSpanTaxCount++;
                $("#" + gridName + "").setCell(mya[j], CellName, '', { display: 'none' });
            } else {
                rowSpanTaxCount = 1;
                break;
            }
            $("#" + CellName + "" + mya[i] + "").attr("rowspan", rowSpanTaxCount);
        }
    }
}

function MergerSpecifiedLines(gridName, CellName, rowspan) {
    var mya = $("#" + gridName + "").getDataIDs();
    var length = mya.length;
    for (var i = 0; i < length; i++) {
        if (i % 3 != 0) {
            $("#" + gridName + "").setCell(mya[i], CellName, '', { display: 'none' });
        } else {
            $("#" + CellName + "" + mya[i] + "").attr("rowspan", rowspan);
        }
    }

}

//Read the selected row and return the hidden ID column 
function GetCurrentID(GridTableName) {
    var item = $("#" + GridTableName);
    var id = $("#" + GridTableName).getCell($("#" + GridTableName).getGridParam("selrow"), "ID");
    return id;
}

function GetMultiSelectedIDs(GridTableName) {
    var selr = $('#' + GridTableName).jqGrid('getGridParam', 'selarrrow');
    var ids = "";
    for (var i = 0; i < selr.length; i++) {
        if (ids == "") {
            ids = $("#" + GridTableName).getCell(selr[i], "ID");
        } else {
            ids = ids + "," + $("#" + GridTableName).getCell(selr[i], "ID");
        }
    }
    return ids;
}


function GetCellContent(GridTableName, ColName) {
    var _sel = $("#" + GridTableName).jqGrid("getGridParam", "selrow");
    console.log("selarrrow=" + _sel);

    //var name = $("#" + GridTableName).getCell($("#" + GridTableName).getGridParam("selrow"), ColName);
    var name = $("#" + GridTableName).getCell(_sel, ColName);
    console.log("name=" + name);
    return name;
}

function GetMultiSelectedCell(GridTableName, ColName) {
    var selr = $('#' + GridTableName).jqGrid('getGridParam', 'selarrrow');
    var ids = "";
    for (var i = 0; i < selr.length; i++) {
        if (ids == "") {
            ids = $("#" + GridTableName).getCell(selr[i], ColName);
        } else {
            ids = ids + "," + $("#" + GridTableName).getCell(selr[i], ColName);
        }
    }
    return ids;
}

function GetDblClickRow(GridTableName) {
    var row = $(event.target).closest("tr.jqgrow").attr("id");
    return row;
}

function GetDblClickCell(GridTableName, ColName) {
    var row = GetDblClickRow("#" + GridTableName);
    var value = $("#" + GridTableName).getCell(row, ColName);
    return value;
}

function GetAllValues(GridTableName, ColName) {
    var _rows = $("#" + GridTableName).jqGrid("getDataIDs")
    var _value = "";
    for (var i = 0; i < _rows.length; i++) {
        _value = _value == "" ? $("#" + GridTableName).getCell(_rows[i], ColName) : _value + "," + $("#" + GridTableName).getCell(_rows[i], ColName);
    }
    return _value;
}