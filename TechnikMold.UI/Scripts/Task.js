﻿$("document").ready(function () {
    var _listsize = Math.round((document.documentElement.clientHeight) / 24.5);
    $("#MoldSelect").attr("size", _listsize);


    $("#AddTask").on("click", function () {
        //ClearEditDialog();
        //GetProjectID($("#MoldNumber").val());
        //$("#EditTaskDialog").modal("show");
        $("#MoldSelectDialog").modal("show");
    })

    $("#CreateTask").on("click", function () {
        //$("#CreateTaskForm").submit();
        $("#CreateTaskDialog").modal("hide");
        CreateCAMTask();
    })

    $("#SelProject").on("click", function () {
        $("#ProjectSelect").modal("show");
    })

    $("#MoldList").on("dblclick", function () {
        SelectProject();
    });

    $("#EditTask").on("click", function () {
        SaveMachineTask();
    });

    $("#SaveCncPara").on("click", function () {
        SaveCncPara();
    });

    //$("#SaveEDMPara").on("click", function () {
    //    SaveEDMPara();
    //});

    $("#CreateEDMItemBtn").on("click", function () {
        SaveEDMItem("CreateEDMItemForm");
    })

    $("#SaveEDMItemBtn").on("click", function () {
        SaveEDMItem("EditEDMItemForm")
    })

    $("#SaveQCInfo").on("click", function () {
        SaveQCInfo();
    });

    $("#SetPosition").on("click", function () {
        EletrodePositionSetup();
    });

    $("#TaskName").on("change", function () {
        CheckTaskExist($("#TaskName").val());
    })

    $("#AddCNCTask").on("click", function () {
        location.href = "/Task/AddCNCTask";
    })

    ///-------------Machine Task List Menu Start------------
    $("#ReloadGrid").on("click", function () {
        location.reload();
    });

    $("#AddToQueue").on("click", function () {
        var _tasktype = $("#CurrentTaskType").val();
        switch (_tasktype) {
            case "2":
                StartEDMTask();
                break;
            case "4":
                StartSteelTask();
                break;
            case "6":
                StartGrindTask();
                break;
        }
    });
    //Define priority level of selected tasks
    $(".priority").on("click", function () {
        SetPriority(GetMultiSelectedIDs("TaskGrid"), this.id);
    })

    $("#CNCItemList").on("click", function () {
        ShowCNCItemList();
    })

    ///Add select items to print queue
    $("#PrintLabel").on("click", function () {
        var selIDs = GetMultiSelectedCell("TaskItemGrid", "CNCItemID");
        if (selIDs == "") {
            alert("请至少选择一个电极");
        } else {
            PrintLabels(selIDs);
            //window.close();
        }        
    })

    $("#OutSource").on("click", function () {
        var _ids = GetMultiSelectedIDs("TaskGrid");
        if (_ids == "") {
            alert("请选择至少一个任务");
        } else {
            if (confirm("外发选中任务？")) {
                var _ids = GetMultiSelectedIDs("TaskGrid");
                Outsource(_ids);
               
            }
        }
    })

    $("#CancelOutSource").on("click", function () {
        var _ids = GetMultiSelectedIDs("TaskGrid");
        if (_ids == "") {
            alert("请选择至少一个任务");
        } else {
            if (confirm("取消选中任务外发？")) {
                CancelOutSource(_ids);
                $("#TaskGrid").jqGrid().trigger("reloadGrid");
            }
        }

    });

    $("#ScanBarcode").on("click", function () {
        $("#BarCode").val("");
        $("#Position").val("");
        $("#ScanBarCodeDialog").modal("show");
        $("#BarCode").focus();
    });

    $("#EditEDMItemForm #EDMItemID").on("change", function () {
        var _id = $("#EditEDMItemForm #EDMItemID option:selected").val();
        $("#EditEDMItemForm #EDMTaskName").val($("#EditEDMItemForm #EDMItemID option:selected").text());
        LoadEDMItem(_id);
    });


    //---------EDM Program Generation Start-----------------

    $("#EDMProcessList").on("click", function () {
        var ids = GetMultiSelectedIDs("TaskGrid");
        var _ids = ids.split(',');
        if (ids != "") {
            location.href = "/Task/EDMProcessList?TaskID=" + _ids[0];
        } else {
            alert("请至少选择一项加工任务")
        }
        
    })

    $("#AddItem").on("click", function () {
        AddEDMitemBatch();
    })

    $("#RemoveItem").on("click", function () {
        RemoveEDMItem();
    })

    $("#DeviceSelect").on("click", function () {
        $("#DeviceSelectDialog").modal("show");
    })

    $("#SaveDevice").on("click", function () {
        $("#DeviceSelect").html($("#DeviceType option:selected").text());
        $("#DeviceSelectDialog").modal("hide");
    })

    //显示抬高高度对话框
    $("#RaiseHeight").on("click", function () {
        RaiseHeightDialog();
    })

    //设置抬高高度
    $("#SaveRaiseHeight").on("click", function () {
        SaveRaiseHeight();
    })

    //显示工件个数对话框
    $("#PieceCount").on("click", function () {
        PieceCountDialog();
    })

    $("#SavePieceCount").on("click", function () {
        SetPieceCount();
    })


    //显示电极位置对话框
    $("#ItemPosition").on("click", function () {
        EDMItemPositionDlg();
    });

    //设置电极位置
    $("#SetElePosition").on("click", function () {
        SetElePosition();
    })


    //显示表面要求对话框
    $("#ItemSurface").on("click", function () {
        ItemSurfaceDlg();
    });
    //设置表面要求
    $("#SetItemSurface").on("click", function () {
        SetItemSurface();
    })

    //显示补偿值对话框
    $("#Compensation").on("click", function () {
        CompensationSetDlg();   
    });
    //设置补偿值
    $("#SetCompensationPara").on("click", function () {
        SetCompensationPara();
        $("#CompensationDlg").modal("hide");
    })
    //显示平动方式对话框
    $("#ObitSelect").on("click", function () {
        ObitDlg();
    });

    //设置平动方式值
    $("#SetObit").on("click", function () {
        SetObit();
        $("#ObitDlg").modal("hide");
    })

    //显示材料对话框
    $("#MaterialSelect").on("click", function () {
        MaterialDlg();
    });
    //设置材料值
    $("#SetMaterial").on("click", function () {

        SetMaterial();
        $("#MaterialDlg").modal("hide");
    });
    
    //生成程序
    $("#CreateProgram").on("click", function () {
        var selr = $('#ProcessGrid').jqGrid('getGridParam', 'selarrrow');
        if (selr != "") {
            
            if (($("#DeviceSelect").text() == "夏米尔350") || ($("#DeviceSelect").text() == "夏米尔350选择跑位")) {
                $("#FileName").val("O0001.iso");
                $("#MachSequence").modal("show");
            } else {
                $("#FileName").val(".CMD");
                //alert(($("#DeviceSelect").text() == "夏米尔350 选择跑位") || (($("#DeviceSelect").text() == "夏米尔23 选择跑位")));
                if ($("#DeviceSelect").text().indexOf("选择跑位")>=0) {
                    ShowPositionSelect();
                } else {
                    GetProgram();
                }
                
            }
        } else {
            alert("请至少选择一个加工项");
        }
    })


    $("#SequenceSetButton").on("click", function () {
        if ($("#DeviceSelect").text().indexOf("选择跑位") >= 0) {
            ShowPositionSelect();
        } else {
            GetProgram();
        }
        
        $("#MachSequence").modal("hide");
    })

    $("#PositionSetButton").on("click", function () {
        GetProgram();
    })

    $("#ElePosList").on("change", function () {
        ShowPosition();
    })

    $("#DownloadFile").on("click", function(){
        DownloadFile();
    })




    //---------EDM Program Generation End-----------------


    ///-------------Machine Task List Menu End------------



    $("#FinishCNCTask").on("click", function () {
        var ids = GetMultiSelectedIDs("TaskGrid");
        if (ids == "") {
            alert("请至少选择一个任务");
        } else {
            FinishCNCTask(ids);
        }
    })

    $("#FinishTask").on("click", function () {
        FinishTask(GetMultiSelectedIDs("TaskGrid"))
    })

    $("#SetAccept").on("click", function () {
        SetTaskAccept()
    })


    $(".positive").on("blur", function () {
        if (ValidatePositive(this.id)) {
            $("#EditTask").attr("disabled", false);
        } else {
            $("#EditTask").attr("disabled", true);
        }
    })

    $("#EleList").on("change", function () {
        var taskid = $("#EleList option:selected").val();
        UpdateEleMachInfo(taskid);
    })

    $("#EleList").on("dblclick", function () {
        var taskid = $("#EleList option:selected").val();
        ShowElePDF(taskid);
    })

    $("#MachineSetting").on("click", function () {
        location.href = "/Task/MachineSetting";
    })

    $("#MachineList").on("change", function(){
        LoadMachineInfo($("#MachineList option:selected").val());
    })

    $("#CreateMachine").on("click", function () {
        CreateMachine();
    })
    
    $("#DeleteMachine").on("click", function(){
        DeleteMachine();
    })

    $("#AvailableElectrodeList").on("dblclick", function () {
        var _item = $("#AvailableElectrodeList option:selected").text();
        $("#AvailableElectrodeList option:selected").remove();
        $("#SelectedElectrodeList").append($("<option/>", {
            text: _item,
            value: _item
        }))        
    })

    $("#SelectedElectrodeList").on("dblclick", function () {
        var _item = $("#SelectedElectrodeList option:selected").text();
        $("#SelectedElectrodeList option:selected").remove();
        $("#AvailableElectrodeList").append($("<option/>", {
            text: _item,
            value: _item
        }))
    })

    $("#ScanBarCodeDialog").on("shown.bs.modal", function () {
        LoadMachine();
    })

    $("#PointCheck").on("click", function () {
        location.href = "/Task/TaskFinishList?TaskType=1";
    })

    $("#EleQCResult").on("click", function () {
        location.href = "/Task/GetEleQCResult";
    })

    $("#SaveElectrode").on("click", function () {
        SaveCNCItemResult();
    })

    $("#EleInStock").on("click", function () {
        location.href = "/Task/EleInStock";
    })

    $("#CloseTask").on("click", function () {
        CloseTask();
    })
    
    $("#DeleteTask").on("click", function(){
        ShowDeleteByCNC();
    })

    $("#ConfirmDeleteTask").on("click", function () {
        DeleteTaskByCNC();
    })

    $("#DeleteEDMTask").on("click", function () {
        DeleteTaskByEDM();
    })

    $("#QueryTask").on("click", function () {
        QueryTaskByKeyword();
    })

    $("#ConfirmFinishEDM").on("click", function () {
        ConfirmFinishEDM();
    })

    $("#RefreshMold").on("click", function () {
        var curval = $("#MoldSelect").val();
        if (curval == null) {
            curval = "";
        }
        //LoadMoldList(1, @ViewBag.State, @ViewBag.TaskType, $("#Keyword").val(), "");       
        //function LoadMoldList(CAM, State,TaskType, Keyword, CurrentVal) {
        LoadMoldList($("#FCAM").val(),$("#FState").val(),$("#FTaskType").val(), $("#Keyword").val(), curval);
    })

    
});


//图纸发布任务列表

function TaskListCAM(TaskType) {
    $("#TaskGrid").setGridParam().showHideCol("CreateTime");
    //$("#TaskGrid").setGridParam().showHideCol("AcceptTime");
    $("#TaskGrid").setGridParam().showHideCol("CAM");

    switch (TaskType) {
        case 1:
            $("#TaskGrid").setGridParam().showHideCol("QCPoints");
            $("#TaskGrid").setGridParam().showHideCol("PosCheck");
            break;
        case 2:
            $("#TaskGrid").setGridParam().showHideCol("QCPoints");
            break;
        case 3:
            break;
        case 4:
            $("#TaskGrid").setGridParam().showHideCol("QCPoints");
            break;
        case 5:
            break;
        case 6:
            break;

    }

    $("#TaskGrid").setGridWidth(document.body.clientWidth * 0.75);
}

///CAM加工状态列表
//CNC任务列表
function TaskListCNC() {
    $("#TaskGrid").setGridParam().showHideCol("Priority");
    $("#TaskGrid").setGridParam().showHideCol("Material");
    $("#TaskGrid").setGridParam().showHideCol("R");
    $("#TaskGrid").setGridParam().showHideCol("F");
    $("#TaskGrid").setGridParam().showHideCol("State");
    $("#TaskGrid").setGridParam().showHideCol("Raw");
    $("#TaskGrid").setGridParam().showHideCol("QCPoints");
    $("#TaskGrid").setGridParam().showHideCol("PlanTime");

    $("#TaskGrid").setGridWidth(document.body.clientWidth * 0.8);
}

function TaskListCNCHistory() {

}

//EDM任务列表
function TaskListEDM() {
    $("#TaskGrid").setGridParam().showHideCol("PlanTime");
    $("#TaskGrid").setGridParam().showHideCol("Priority");
    $("#TaskGrid").setGridParam().showHideCol("Quantity");
    $("#TaskGrid").setGridParam().showHideCol("State");
    $("#TaskGrid").setGridParam().showHideCol("ForecastTime");
    $("#TaskGrid").setGridParam().showHideCol("AcceptTime");
    $("#TaskGrid").setGridParam().showHideCol("StartTime");
    $("#TaskGrid").setGridParam().showHideCol("CreateTime");
    $("#TaskGrid").setGridParam().showHideCol("StateMemo");
    $("#TaskGrid").setGridWidth(document.body.clientWidth * 0.8);
}

function TaskListEDMHistory() {

}


//WEDM任务列表
function TaskListWEDM() {
    $("#TaskGrid").setGridParam().showHideCol("Time");
    $("#TaskGrid").setGridParam().showHideCol("State");
    $("#TaskGrid").setGridParam().showHideCol("Priority");
    $("#TaskGrid").setGridParam().showHideCol("Quantity");
    $("#TaskGrid").setGridParam().showHideCol("StateMemo");
    $("#TaskGrid").setGridParam().showHideCol("AcceptTime");
    $("#TaskGrid").setGridParam().showHideCol("PlanTime");
    $("#TaskGrid").setGridParam().showHideCol("StartTime");
    $("#TaskGrid").setGridParam().showHideCol("ReleaseTime");
    $("#TaskGrid").setGridWidth(document.body.clientWidth * 0.8);
}

function TaskListWEDMHistory() {

}


//铣铁任务列表
function TaskListMill() {
    $("#TaskGrid").setGridParam().showHideCol("State");
    $("#TaskGrid").setGridParam().showHideCol("Priority");
    $("#TaskGrid").setGridParam().showHideCol("ProcessName");
    $("#TaskGrid").setGridParam().showHideCol("Quantity");
    $("#TaskGrid").setGridParam().showHideCol("CreateTime");
    $("#TaskGrid").setGridParam().showHideCol("StateMemo");
    $("#TaskGrid").setGridParam().showHideCol("PlanTime");
    $("#TaskGrid").setGridWidth(document.body.clientWidth * 0.8);
}

function TaskListMillHistory() {

}

//铣磨任务列表
function TaskListGrind() {
    $("#TaskGrid").setGridParam().showHideCol("State");
    $("#TaskGrid").setGridParam().showHideCol("Priority");
    $("#TaskGrid").setGridParam().showHideCol("ProcessName");
    $("#TaskGrid").setGridParam().showHideCol("Quantity");
    $("#TaskGrid").setGridParam().showHideCol("CreateTime");
    $("#TaskGrid").setGridParam().showHideCol("StateMemo");
    $("#TaskGrid").setGridParam().showHideCol("PlanTime");
    $("#TaskGrid").setGridWidth(document.body.clientWidth * 0.8);
}

//Display the mold select dialog
function ShowMoldSelect() {
    $("#ProjectSelect").modal("show");
}


//Add a new CAM task via ajax submit
function CreateCAMTask() {
    $.ajax({
        type: "Post",
        dataType: "html",
        url:"/Task/SaveCAMTask",
        data:$("#CreateTaskForm").serialize(), 
        error: function () {

        },
        success: function (msg) {
            if (msg == 0) {
                alert("任务保存失败!");
            } else {
                alert("任务保存成功");
                
                $("#TaskGrid").jqGrid().trigger("reloadGrid");
            }
        }
    });
}

function SetTaskAccept() {
    var id = GetMultiSelectedIDs("TaskGrid")
    $.ajax({
        dataType: "html",
        url: "/Task/AcceptMachTask?TaskIDs="+id,
        error: function () { },
        success: function (msg) {
            if(msg ==""){                
                alert("工件接受成功");
                location.reload();
                
            }
        }
    })
    
}

function ReleaseTask(id) {
    $.ajax({
        dataType: "html",
        url: "/Task/ReleaseCAMTask?TaskID=" + id,
        error: function () { },
        success: function (msg) {
            switch (msg) {
                case "1":
                    alert("任务接受成功");
                    location.reload();
                    break;
                case "2":
                    alert("请完成当前任务后再接受新任务");
                    break;
                case "3":
                    alert("当前任务已经被接受");
                    break;
                case "99":
                    alert("访客无法接受任务");
                    break;
            }
        }

    })
}

function SaveMachineTask() {
    if (ValidateCreate("EditTaskDialog")) {
        $.ajax({
            type: "Post",
            dataType: "html",
            url: "/Task/SaveTask",
            data: $("#CreateTaskForm").serialize(),
            error: function () {

            },
            success: function (msg) {
                if (msg == 0) {
                    alert("创建失败!");
                } else {
                    if ($("#TaskID").val() == 0) {
                        alert("创建任务成功");
                    } else {
                        alert("任务保存成功");
                    }
                    $("#EditTaskDialog").modal("hide");

                }
                location.reload();
            }
        });
    }
    
}

function AcceptCAMTask(id) {
        $.ajax({
            dataType: "html",
            url: "/Task/AcceptTask?TaskID=" + id,
            error: function () { },
            success: function (msg) {
                switch (msg) {
                    case "1":
                        alert("任务接受成功");                       
                        break;
                    case "2":
                        alert("请完成当前任务后再接受新任务");
                        break;
                    case "3":
                        alert("无法接受已经被接受的任务");
                        break;
                    case "99":
                        alert("访客无法接受任务");
                        break;

                }
                location.reload();
            }
        })
}


function ReleaseCAMTask(id) {
    if (confirm("确认发布任务?")){
        $.ajax({
            dataType: "html",
            url: "/Task/ReleaseTask?TaskID=" + id,
            error: function () { },
            success: function (msg) {
                switch (msg) {
                    case "1":
                        alert("任务发布成功");
                        location.reload();
                        break;
                    case "10":
                        alert("此任务不是您认领的任务");
                        break;
                    case "11":
                        alert("无跑位数据的任务不能发布，请输入后发布");
                        break;
                    case "12":
                        alert("无QC点信息的任务不能发布，请输入后发布");
                        break;
                    case "13":
                        alert("无图纸的任务不能发布，请先生成图纸");
                        break;
                }
            }
        });
    }
}



//Display the CNC item parameter dialog
function CNCPara(taskID) {

        $("#EditCNCParaForm #TaskID").val(taskID);
        $.getJSON("/Task/JsonCNCPara?TaskID=" + taskID, function (msg) {
            var value = "";
            $("#CNCParameterID").val(msg.CNCParameterID);
            $("#TaskID").val(msg.TaskID);
            $("#Position").val(msg.Position);
            $("#Surface").val(msg.Surface);
            if (msg.RoughGap != 0) {
                value = msg.RoughGap;
            } else {
                value = "";
            }
            $("#RoughGap").val(value);
            if (msg.FinishGap != 0) {
                value = msg.FinishGap;
            } else {
                value = "";
            }
            $("#FinishGap").val(value);
            $("#ObitType").val(msg.ObitType);
            $("#NCRoughName").val(msg.NCRoughName);
            $("#NCFinishName").val(msg.NCFinishName);
            
            if (msg.Preserve != 0) {
                
                value = Math.round(msg.Preserve);
            } else {                
                value = "";
            }
            $("#Preserve").val(value);
        })    
        $("#CNCParaDialog").modal("show");
}


//function EDMItem(EDMItemID) {
//    //$("#EditEDMParaForm #TaskID").val(EDMItemID);
//    $.getJSON("/Task/JsonEDMPara?TaskID=" + EDMItemID, function (msg) {
//        $("#EDMParaID").val(msg.EDMItemID);
//        $("#TaskID").val(msg.TaskID);
//        $("#EDMTaskName").val(msg.LabelName);
//        $("#Gap").val(msg.Gap);
//        $("#OffsetX").val(msg.OffsetX);
//        $("#OffsetY").val(msg.OffsetY);
//        $("#OffsetZ").val(msg.OffsetZ);
//        $("#OffsetC").val(msg.OffsetC);
//        $("#GapCompensate").val(msg.GapCompensate);
//        $("#ZCompensate").val(msg.ZCompensate);
//        $("#ElePosition").val(msg.ElePosition);
//        $("#Surface").val(msg.Surface);
//        $("#Material").val(msg.Material);
//        $("#Obit").val(msg.Obit);
//    })
//    $("#EDMParaDialog").modal("show");
//}

function CreateEDMItem(TaskID) {
    $("#CreateEDMItemForm #TaskID").val(TaskID)
    $("#CreateEDMItemDialog").modal("show");
}

function EditEDMItem(TaskID) {
    $("#EditEDMItemForm #EDMItemID option").remove;
    $("#EditEDMItemForm #EDMItemID").append($("<option/>", { value: 0, text: "请选择EDM工作项" }));
    $.getJSON("/Task/JsonEDMItems?TaskID=" + TaskID, function (msg) {
        $.each(msg, function (i, n) {
            $("#EditEDMItemForm #EDMItemID").append($("<option/>", {
                value: n.EDMItemID,
                text: n.LabelName
            }))
        });
    })
    $("#EDMItemsDialog").modal("show");
}





function QCInfo(taskID) {
    $("#SaveQCInfoForm #TaskID").val(taskID);
    $.getJSON("/Task/JsonQCInfo?TaskID=" + taskID, function (msg) {
       
        $("#QCInfoID").val(msg.QCInfoID);

        $("#QCPoints").val(msg.QCPoints);
    })
    $("#QCInfoDialog").modal("show");
}

function SaveCncPara() {
    
    if (ValidateCreate("EditCNCParaForm")) {
            if ($("#CNCItemID").val() != 0) {
                $.ajax({
                    dataType: "html",
                    url: "/Task/SaveCncPara",
                    data: $("#EditCNCParaForm").serialize(),
                    error: function () { },
                    success: function (msg) {
                        alert("电极加工参数保存成功");
                        $("#CNCParaDialog").modal("hide");
                        location.reload();
                    }

                })
            } else {
                alert("请先选择电极");
            }
        
    } else {
        alert("请填写黄色必填项");
    }
}

function SaveEDMItem(FormID) {
    
        $.ajax({
            type: "Post",
            dataType: "html",
            url: "/Task/SaveEDMItems",
            data: $("#"+FormID).serialize(),
            error: function () {

            },
            success: function (msg) {
                alert(msg==0);
                switch (msg) {
                    case "0":
                        alert("任务创建成功");
                        location.reload();
                        break;
                    case "1":
                        alert("任务创建失败,请重试");
                        break;
                }                
            }
        });
    
}

function SaveQCInfo() {
    if (ValidateCreate("SaveQCInfoForm")) {
        $.ajax({
            dataType: "html",
            url: "/Task/SaveQCInfo",
            data: $("#SaveQCInfoForm").serialize(),
            error: function () { },
            success: function (msg) {
                alert("质检参数保存成功");
                $("#QCInfoDialog").modal("hide");
                location.reload();
            }
        })
    } else {
        alert("请填写黄色必填项");
    }
}



function ValidateCreate(FormName) {
    var RequiredFieldValid = true;
    var PhaseDateValid = true;
    var errorMessage = "";

    var selector = "input.required";
    if (FormName != "") {
        selector = "#" + FormName + " :input.required"
    }

    //Required field is filled
    $(selector).each(function () {
        var item = $("#" + UnifyName(this.id));
        if (item.val() == "") {
            item.addClass("invalidefield");
            RequiredFieldValid = false;
        } else {
            item.removeClass("invalidefield");
        }
    });
    return RequiredFieldValid;
}

function PauseTask(TaskID)
{
    $.ajax({
        dataType: "html",
        url: "/Task/PauseTask?TaskID="+TaskID,
        error: function () { },
        success: function (msg) {
            alert(msg);
            location.reload();
        }
    })
}


function DeleteTaskByCAM(TaskID) {
    $.ajax({
        dataType: "html",
        url: "/Task/DeleteTaskByCAM?TaskID=" + TaskID,
        error: function () { },
        success: function (msg) {
            alert(msg);
            location.reload();
        }
    })
}

function ShowDeleteByCNC() {
    $("#TaskIDs").val(GetMultiSelectedIDs("TaskGrid"));
    $("#DeleteByCNC").modal("show");
}


function DeleteTaskByCNC() {
    var _taskIDs = $("#TaskIDs").val();
    var _memo = $("#Memo").val();
    if (_taskIDs != "") {
        $.ajax({
            dataType: "html",
            url: "/Task/DeleteTaskByCNC?TaskIDs=" + _taskIDs+"&Memo="+_memo,
            error: function () { },
            success: function (msg) {
                if (msg != "") {
                    alert("以下任务不允许删除:" + msg);
                    
                } else {
                    location.reload();
                }
                
            }
        })
    } else {
        alert("请至少选择一个任务");
    }
}

function DeleteTaskByEDM() {
    var _taskIDs = GetMultiSelectedIDs("TaskGrid");
    if (_taskIDs != "") {
        if (confirm("确认删除EDM任务?")) {
            $.ajax({
                dataType: "html",
                url: "/Task/DeleteTaskByEDM?TaskIDs=" + _taskIDs,
                error: function () { },
                success: function (msg) {
                    if (msg != "") {
                        alert("以下任务不允许删除:" + msg);
                        
                    } else {
                        location.reload();
                    }

                }
            })
        }        
    } else {
        alert("请至少选择一个任务");
    }
}






function QueueTask(TaskIDs) {
    var items = TaskIDs;

    $("#SteelProgramSelectDialog").modal("show");

    GetTasks(TaskIDs);
    //if (confirm("将选中任务加入加工队列?")) {
    //    $.ajax({
    //        dataType: "html",
    //        url: "/Task/QueueTask?TaskIDs=" + TaskIDs,
    //        error: function () { },
    //        success: function (msg) {
    //            if (msg == "") {
    //                alert("所选任务进入加工等待状态");
    //            } else {
    //                alert(msg);
    //            }
    //            location.reload();
    //        }
    //    })
    //}
}

///Set the priority level of selected tasks
function SetPriority(TaskIDs, Level) {
    if (confirm("要将选中任务优先级设为" + Level + "?")) {
        $.ajax({
            dataType: "html",
            url: "/Task/SetTaskPriority?TaskIDs=" + TaskIDs + "&Level=" + Level,
            error: function () { },
            success: function (msg) {
                if (msg = true) {
                    
                } else {
                    alert("出现错误,请检查并重新设置任务优先级");
                }
                location.reload();
            }
        });
    }  
}

function Outsource(ids) {
    $.ajax({
        dataType: "html",
        url: "/Task/OutSource?TaskIDs=" + ids,
        error: function (msg) {
            alert(msg);
        },
        success: function (msg) {
            if (msg == "") {
                location.href = "/Purchase/PRDetail?TaskIDs=" + ids + "&MoldNumber=" + $("#MoldSelect").val()+"&TaskType="+$("#CurrentTaskType").val();
            } else {
                alert(msg);
                return false;
            }
        }
    })
}


function CancelOutSource(ids) {
    $.ajax({
        dataType: "html",
        url: "/Task/CancelOutSource?TaskIDs=" + ids,
        error: function (msg) {
            alert(msg);
        },
        success: function (msg) {
            if (msg == "") {
                alert("取消外发成功");
                location.reload();
            } else {
                alert(msg);
            }
            location.reload();
        }

    })
}



function ShowCNCItemList() {
    var TaskIDs = GetMultiSelectedIDs("TaskGrid")
    if (TaskIDs == "") {
        alert("请至少选择一个任务");
    } else {
        window.open("/Task/CNCItemList?TaskIDs=" + TaskIDs, "_blank", width = 500, height = 400);
    }    
}

function PrintLabels(ItemIDs) {
    var _url = "/Task/AddToPrintQueue?ItemIDs=" + ItemIDs;

    $.ajax({
        dataType: "html",
        url: _url,        
        success: function (msg) {
            if (msg == "") {
                alert("已经进入打印队列");
            } else {
                alert("以下电极标签无法再次打印：" + msg);
            }
            
        }
    })
}


function SetPosition() {
    $.ajax({
        type: "Post",
        dataType: "html",
        url: "/Task/ScanBarCode",
        data: $("#ScanBarCodeForm").serialize(),
        error: function () {
        },
        success: function (msg) {
            alert("电极加工开始");
            $("#BarCode").val("");
            $("#Position").val("");
        }
    });
}

function LoadEDMItem(EDMItemID) {
    $.getJSON("/Task/JsonEDMItem?EDMItemID=" + EDMItemID, function (msg) {
        $("#EditEDMItemForm #TaskID").val(msg.TaskID);
        $("#EditEDMItemForm #Gap").val(msg.Gap);
        $("#EditEDMItemForm #OffsetX").val(msg.OffsetX);
        $("#EditEDMItemForm #OffsetY").val(msg.OffsetY);
        $("#EditEDMItemForm #OffsetZ").val(msg.OffsetZ);
        $("#EditEDMItemForm #OffsetC").val(msg.OffsetC);
        $("#EditEDMItemForm #GapCompensate").val(msg.GapCompensate);
        $("#EditEDMItemForm #ZCompensate").val(msg.ZCompensate);
        $("#EditEDMItemForm #ElePosition").val(msg.ElePosition);
        $("#EditEDMItemForm #Surface").val(msg.Surface);
        $("#EditEDMItemForm #Material").val(msg.Material);
        $("#EditEDMItemForm #Obit").val(msg.Obit);
    })
}




function AddEDMitemBatch() {

    
    var sourceGrid = "#TaskGrid";
    var targetGrid = "#ProcessGrid";




    var selrows = $(sourceGrid).jqGrid('getGridParam', 'selarrrow');
    for (var i = 0; i < selrows.length; i++) {
        //Validate if the data already exists
        var _obj = $("#ProcessGrid").jqGrid("getRowData");
        var _exist = 0;
        $(_obj).each(function () {
            if (this.LableName == $(sourceGrid).getCell(selrows[i], "LabelName")) {
                _exist = 1;
            }
        })
        if (_exist==0){
        var data = {
            ID: $(sourceGrid).getCell(selrows[i], "ID"),
            ELEName: $(sourceGrid).getCell(selrows[i], "ELEName"),
            LableName: $(sourceGrid).getCell(selrows[i], "LabelName"),
            Position: i+1,
            Gap: $(sourceGrid).getCell(selrows[i], "Gap"),
            OffsetX: $(sourceGrid).getCell(selrows[i], "OffsetX"),
            OffsetY: $(sourceGrid).getCell(selrows[i], "OffsetY"),
            OffsetZ: $(sourceGrid).getCell(selrows[i], "OffsetZ"),
            OffsetC: $(sourceGrid).getCell(selrows[i], "OffsetC"),
            GapCompensate: $(sourceGrid).getCell(selrows[i], "GapCompensate"),
            ZCompensate: $(sourceGrid).getCell(selrows[i], "ZCompensate"),
            Surface: $(sourceGrid).getCell(selrows[i], "Surface"),
            Obit: $(sourceGrid).getCell(selrows[i], "Obit"),
            Material: $(sourceGrid).getCell(selrows[i], "Material"),
            ElePoints:$(sourceGrid).getCell(selrows[i], "ElePoints"),
            EleType: $(sourceGrid).getCell(selrows[i], "EleType"),
            StockGap: $(sourceGrid).getCell(selrows[i], "StockGap"),
            CNCMachMethod:$(sourceGrid).getCell(selrows[i], "CNCMachMethod"),

        }
        $(targetGrid).addRowData($(targetGrid).getGridParam("reccount") + 1, data, 0, 0);
        } else {
            alert("电极已添加");
        }
    }
}

function RemoveEDMItem() {
    var targetGrid = "#ProcessGrid";

    
    var selrows = $(targetGrid).jqGrid('getGridParam', 'selarrrow');
    for (var i = selrows.length + 1; i >= 0; i--) {
        $(targetGrid).jqGrid("delRowData", selrows[i]);
    }


}

//显示电极位置对话框
function EDMItemPositionDlg() {
    var targetGrid = "#ProcessGrid";
    var selrows = $(targetGrid).jqGrid('getGridParam', 'selarrrow');
    $("#curPosition").val($(targetGrid).getCell(selrows[0], 2));
    $("#PositionDlg").modal("show");
}

//设置当前选中行电极位置
function SetElePosition() {
    var targetGrid = "#ProcessGrid";
    var selrows = $(targetGrid).jqGrid('getGridParam', 'selarrrow');
    var posval = $("#curPosition").val();
    var totalrows = $(targetGrid).getGridParam('reccount');
    var posgap = $("#PositionGap").val();
    var rowno = selrows[0];
    var a = 0;
    for (var i = selrows[0]; i < totalrows+1 ; i++) {
        $(targetGrid).setCell(rowno, 2, posval);
        rowno = 1+Number( rowno) ;
        posval = Number(posgap) + Number(posval);
    }

    
    $("#PositionDlg").modal("hide");
}

//显示抬高高度对话框
function RaiseHeightDialog() {
    $("#RaiseHeightDialog").modal("show");
}

//设置抬高高度
function SaveRaiseHeight() {
    $("#RaiseHeight").html("抬高高度:" + $("#RaiseHeightValue").val());
    $("#RaiseHeightDialog").modal("hide");
}

//显示工件数量对话框
function PieceCountDialog() {
    $("#PieceCountDialog").modal("show");
}

//设置工件数量
function SetPieceCount() {
    $("#PieceCount").html("工件个数:" + $("#PieceCountValue").val());
    $("#PieceCountDialog").modal("hide");
}

//显示表面要求对话框
function ItemSurfaceDlg() {
    $("#ItemSurfaceDlg").modal("show");
}

//这只表面要求
function SetItemSurface() {
    var sourceGrid = "#ProcessGrid";
    var selrows = $(sourceGrid).jqGrid('getGridParam', 'selarrrow');
    for (var i = 0; i < selrows.length; i++) {
        $(sourceGrid).setCell(selrows[i], "Surface", $("#SurfaceVal option:selected").val());
    }
    $("#ItemSurfaceDlg").modal("hide");
}


//显示补偿值对话框
function CompensationSetDlg() {
    $("#CompensationDlg").modal("show");
}

//设置补偿值
function SetCompensationPara() {
    var sourceGrid = "#ProcessGrid";
    var selrows = $(sourceGrid).jqGrid('getGridParam', 'selarrrow');
    for (var i = 0; i < selrows.length; i++) {
        $(sourceGrid).setCell(selrows[i], "GapCompensate", $("#GapCompensationVal").val());
        $(sourceGrid).setCell(selrows[i], "ZCompensate", $("#ZCompensationVal").val());
    }
}

//显示平动方式对话框
function ObitDlg() {
    $("#ObitDlg").modal("show");
}

//设置平动方式值
function SetObit() {
    var sourceGrid = "#ProcessGrid";
    var selrows = $(sourceGrid).jqGrid('getGridParam', 'selarrrow');
    for (var i = 0; i < selrows.length; i++) {
        $(sourceGrid).setCell(selrows[i], "Obit", $("#ObitList option:selected").val());
    }
}

//显示材料对话框
function MaterialDlg() {
    $("#MaterialDlg").modal("show");
}
//设置材料值
function SetMaterial() {
    var sourceGrid="#ProcessGrid";
    var selrows = $(sourceGrid).jqGrid('getGridParam', 'selarrrow');
    for (var i=0;i<selrows.length;i++){
        $(sourceGrid).setCell(selrows[i], "Material", $("#MaterialList option:selected").text());
    }
    
}

///提交EDM数据并获取程序结果
function GetProgram() {
    var sourceGrid = "#ProcessGrid";
    var selrows = $(sourceGrid).jqGrid('getGridParam', 'selarrrow');
    var itemData = "";
    var name = "Items";
    for (var i = 0; i < selrows.length; i++) {
        
        itemData = itemData + name + "[" + i + "].ID=" + $(sourceGrid).getCell(selrows[i], "ID") + "&" +
            name + "[" + i + "].ELEName=" + $(sourceGrid).getCell(selrows[i], "ELEName") + "&" +
            name + "[" + i + "].LableName=" + $(sourceGrid).getCell(selrows[i], "LableName") + "&" +
            name + "[" + i + "].Gap=" + $(sourceGrid).getCell(selrows[i], "Gap") + "&" +
            name + "[" + i + "].OffsetX=" + $(sourceGrid).getCell(selrows[i], "OffsetX") + "&" +
            name + "[" + i + "].OffsetY=" + $(sourceGrid).getCell(selrows[i], "OffsetY") + "&" +
            name + "[" + i + "].OffsetZ=" + $(sourceGrid).getCell(selrows[i], "OffsetZ") + "&" +
            name + "[" + i + "].OffsetC=" + $(sourceGrid).getCell(selrows[i], "OffsetC") + "&" +
            name + "[" + i + "].GapCompensate=" + $(sourceGrid).getCell(selrows[i], "GapCompensate") + "&" +
            name + "[" + i + "].ZCompensate=" + $(sourceGrid).getCell(selrows[i], "ZCompensate") + "&" +
            name + "[" + i + "].ElePosition=" + $(sourceGrid).getCell(selrows[i], "Position") + "&" +
            name + "[" + i + "].Surface=" + $(sourceGrid).getCell(selrows[i], "Surface") + "&" +
            name + "[" + i + "].Material=" + $(sourceGrid).getCell(selrows[i], "Material") + "&" +
            name + "[" + i + "].Obit=" + $(sourceGrid).getCell(selrows[i], "Obit") + "&" +
            name + "[" + i + "].CNCMachMethod=" + $(sourceGrid).getCell(selrows[i], "CNCMachMethod") + "&" +
            name + "[" + i + "].ELEName=" + $(sourceGrid).getCell(selrows[i], "ELEName") + "&" +
            name + "[" + i + "].ElePoints=" + $(sourceGrid).getCell(selrows[i], "ElePoints") + "&" +
            name + "[" + i + "].EleType=" + $(sourceGrid).getCell(selrows[i], "EleType") + "&" +
            name + "[" + i + "].StockGap=" + $(sourceGrid).getCell(selrows[i], "StockGap") + "&" +
            name + "[" + i + "].Position=" + $(sourceGrid).getCell(selrows[i], "Position")+"&";
    }

    itemData = itemData + "DeviceType=" + $("#DeviceType option:selected").val() + "&" +
        "RaiseHeight=" + $("#RaiseHeightValue").val() + "&" +
        "PieceCount=" + $("#PieceCountValue").val() + "&" +
        "MachineName=" + $("#DeviceSelect").text() + "&" +
        "SelectFirst=" + $("#SequenceSet").val();
    //alert(itemData);
    for (i = 0; i < Positions.length; i++) {
        itemData = itemData + "&EleList[" + i + "]=" + EleNames[i] +
            "&PosList[" + i + "]=" + Positions[i];
    }
    

    $.ajax({
        type: "Post",
        dataType: "html",
        url: "/Task/EDMProgram",
        data: itemData,
        error: function () {

        },
        success: function (msg) {

            $("#ProgramContent").html (msg);
            $("#PositionCheckDlg").modal("hide");
            $("#ProgramDlg").modal("show");
        }
    });
}

function ClearEditDialog() {
    $("#TaskName").val("");
    $("#ProcessName").val("");
    $("#Time").val("");
    $("#R").val("");
    $("#F").val("");
    $("#Material").val("");
    $("#Raw").val("");
    $("#DrawingFile").val("");
    $("#Memo").val("");
}

function CNCTaskReady() {
    if (confirm("确认任务准备完毕?")) {
        var ids = GetMultiSelectedCell("TaskItemGrid", "CNCItemID");

        $.ajax({
            type: "Get",
            dataType: "html",
            url: "/Task/CNCItemReady?CNCItemIDs=" + ids,
            success: function (msg) {
                if (msg == "") {
                    alert("任务准备完成");
                }
                location.reload();
            }
        })
    }
}

function SetItemRequired(ids) {
    if (confirm("确认以下零件需要加工?")) {
        $.ajax({
            type: "Get",
            dataType: "html",
            url: "/Task/CNCItemRequired?CNCItemIDs=" + ids,
            success: function (msg) {
                if (msg == "") {
                    alert("任务设置成需要加工");
                }
                location.reload();
            }
        })
    }
}


function SetItemNotRequired(ids) {
    if (confirm("确认以下零件无需加工?")) {        
        $.ajax({
            type: "Get",
            dataType: "html",
            url: "/Task/CNCItemNotRequired?CNCItemIDs=" + ids,
            success: function (msg) {
                if (msg == "") {
                    alert("任务设置成无需加工");
                }
                location.reload();
            }
        })
    }
}




function EditEleCompensation(id) {
    $.getJSON("/Task/JsonEDMItem?EDMItemID=" + id, function (msg) {
        $("#EleName").val(msg.LabelName);
        $("#EDMItemID").val(msg.EDMItemID);
        $("#GapCompensation").val(msg.GapCompensate);
        $("#ZCompensation").val(msg.ZCompensate);
    })

    

    
    $("#EleCompensationDialog").modal("show");
}


function ValidateTaskUser(taskID) {
    var bol;
    $.ajax({
        url: "/Task/ValidateTaskUser?TaskID=" + taskID,
        type: "Get",
        dataType: "html",
        async: false,
        success:function(msg){
            bol = msg;
        }
    })
    return bol;
}


function ValidateTaskAvialable(taskID) {
    var bol;
    $.ajax({
        url: "/Task/ValidateTaskAvaliable?TaskID=" + taskID,
        type: "Get",
        dataType: "html",
        async: false,
        success: function (msg) {
            bol= msg;
        }
    })
    return bol;
}

function FinishCNCTask(TaskIDs) {
    if (confirm("确认将选中任务设为结束？")) {
        $.ajax({
            url: "/Task/SetCNCFinish?CNCTaskIDs=" + TaskIDs,
            type: "Get",
            dataType: "html",
            success: function (msg) {
                if (msg == "") {
                    alert("任务已结束并入库")
                } else {
                    alert(msg);
                }
                location.reload();
            }
        })
    }
}

function FinishTask(TaskIDs) {
    if (confirm("确认将选中任务设为结束？")) {
        $.ajax({
            url: "/Task/SetTaskFinish?TaskIDs=" + TaskIDs,
            type: "Get",
            dataType: "html",
            success: function (msg) {
                if (msg == "") {
                    alert("任务已结束")
                } else {
                    alert("操作失败");
                }
                location.reload();
            }
        })
    }

}

function EletrodePositionSetup() {

    if (($("#BarCode").val() != "") & ($("#Position").val() != "")) {
        $("#Ele_List").append("<tr><td>"+$("#BarCode").val()+"</td><td>"+$("#Position").val()+"</td></tr>");
    }
}

function StartSteelTask() {
    var _id = GetCurrentID("TaskGrid");
    if ((_id == "") || (_id == undefined)) {
        alert("请选择至少一个任务");
    } else {
        GetSteelTasks(_id);
    }
}

function StartGrindTask() {
    var _id = GetMultiSelectedIDs("TaskGrid");
    if ((_id == "") || (_id == undefined)) {
        alert("请选择至少一个任务");
    } else {
        var _url = "/Task/StartGrindTask?TaskIDs=" + _id;
        $.ajax({
            url: _url,
            type: "Get",
            success: function (msg) {
                if (msg == "") {
                    alert("加工任务开始");
                    RefreshTaskGrid("");
                } else {
                    alert(msg);
                }
            }
        })
    }
}

function FinishGrndTask() {
    var _id = GetMultiSelectedIDs("TaskGrid");
    if ((_id == "") || (_id == undefined)) {
        alert("请选择至少一个任务");
    } else {
        var _url = "/Task/FinishGrindTask?TaskIDs=" + _id;
        $.ajax({
            url: _url,
            type: "Get",
            success: function (msg) {
                if (msg == "") {
                    alert("加工任务已设置为结束");
                    RefreshTaskGrid("");
                } else {
                    alert(msg);
                }
            }
        })
    }
}

function StartEDMTask() {
    var _ids = $("#TaskID").val();
    $.ajax({
        url: "/Task/EDMTaskStart?TaskID=" + _ids,
        type: "Get",
        dataType: "html",
        success: function (msg) {
            if (msg == "") {
                alert("任务已设置为开始")
            } else {
                alert(msg);
            }
        }
    })
}

function UpdateEleMachInfo(TaskID) {
    $("#TaskGrid").clearGridData();
    
    $.getJSON("/Task/JsonEDMItems?TaskID=" + TaskID, function (msg) {
        $.each(msg, function (i, n) {
            var data = {
                ID: n.ID,
                ELEName:n.ELEName,
                LabelName: n.LableName,
                Gap: n.Gap,
                OffsetX:n.OffsetX, 
                OffsetY:n.OffsetY, 
                OffsetZ:n.OffsetZ, 
                OffsetC: n.OffsetC,
                GapCompensate: n.GapCompensate,
                ZCompensate: n.ZCompensate,
                Surface: n.Surface,
                Obit: n.Obit,
                Material: n.Material,
                ElePoints: n.ElePoints,
                EleType: n.EleType,
                StockGap: n.StockGap,
                CNCMachMethod:n.CNCMachMethod

            }
            $("#TaskGrid").addRowData(+i, data, 0, 0);
        })
    });
    //$("#TaskGrid").addRow()
}

function ShowPositionSelect() {
    var grid = "ProcessGrid";
    var id = GetMultiSelectedIDs(grid);
    $("#ElePosList option").remove();
    $.getJSON("/Task/GetEDMEleInfo?EleIDs=" + id, function (msg) {
        $.each(msg, function (i, n) {
            $("#ElePosList").append($("<option/>", {
                value: n.TaskID,
                text: n.TaskName
            }))
        })
    })
    $("#PositionCheckDlg").modal("show");
}

function ShowPosition() {
    $("#PositionList").children().remove();
    $.getJSON("/Task/GetElePosition?TaskID=" + $("#ElePosList option:selected").val(), function (msg) {
        $.each(msg, function (i, n) {
            $("#PositionList").append("<tr><td>" + n + "</td><td><input type='checkbox' class='PosSelect' id='" + n + "' checked></td></tr>");
            EleNames.push($("#ElePosList option:selected").text());
            Positions.push(n);
        })
        $(".PosSelect").on("click", function () {
            var item = $(this);
            AddRemovePosition(item[0].id, item[0].checked);
        })
    })
}

function AddRemovePosition(Position, Oper) {
    var _index = -1;
    for (i = 0; i < Positions.length; i++) {
        if (Positions[i] == Position) {
            _index = i;
        }
    }

    if (Oper) {
        if (_index = -1) {
            Positions.push(Position);
            EleNames.push($("#ElePosList option:selected").text());
        }
    } else {
        if (_index >= 0) {
            Positions.splice(_index, 1);
            EleNames.splice(_index, 1);
        }
    }
}

function CheckTaskExist(TaskName) {
    $.ajax({
        url: "/Task/CheckTaskExists?TaskName=" + TaskName,
        type: "Get",
        dataType: "html",
        success: function (msg) {

            if (Number(msg)>-1) {
                if (confirm("系统中已存在同名任务, 是否需要进行任务升版？")) {
                    $("#Version").val( Number(msg) + 1);
                } else {
                    $("#TaskName").addClass("invalidefield");
                }
                
            }
        }
    })
}


function DeleteTask(TaskID) {
    $.ajax({
        url: "/Task/DeleteTask?TaskID=" + TaskID,
        type: "Get",
        dataType: "html",
        success: function (msg) {

            if (Number(msg) > -1) {
                alert("任务删除成功");
                location.reload();
            } else {
                alert("操作失败");
            }
        }
    })
}

function PointCheck(IDs, TaskType) {
    $.ajax({
        url: "/Task/TaskFinish?IDs=" + IDs+"&TaskType="+TaskType,
        type: "Get",
        dataType: "html",
        success: function (msg) {
            if (Number(msg) == 0) {
                alert("任务点检完成");
                location.reload();
            } else {
                alert("操作失败");
            }
        }
    })
}


function LoadMachineInfo(MachineID) {
    $.getJSON("/Task/GetMachineInfo?MachineID=" + MachineID, function (msg) {
        $("#Name").val(msg.Name);
        $("#MachineID").val(msg.MachineID);
        $("#IPAddress").val(msg.IPAddress);
        $("#System_3R").val(msg.System_3R);
        $("#Pallet").val(msg.Pallet);
        $("#PointDescribe").val(msg.PointDescribe);
    })
}

function CreateMachine() {
    $("#Name").val("");
    $("#MachineID").val(0);
    $("#IPAddress").val("");
    $("#System_3R").val("");
    $("#Pallet").val("");
    $("#PointDescribe").val("");
}

function DeleteMachine() {
    if ($("#MachineList option:selected").val() == undefined) {
        alert("请选择要删除的设备")
    } else {
        if (confirm("确认删除设备" + $("#MachineList option:selected").text() + "?")) {
            $.ajax({
                url: "/Task/DeleteMachine?MachineID=" + $("#MachineList option:selected").val(),
                type: "Get",
                dataType: "html",
                success: function (msg) {
                    if (Number(msg) > 0) {
                        alert("设备删除成功");
                        location.reload();
                    } else {
                        alert("设备删除失败");
                    }
                }
            })
        }
        
    }
}

function LoadEleDetail(ID) {
    $.ajax({
        url: "/Task/JsonEleDetail?TaskID="   + ID,
        type: "Get",
        dataType: "html",
        success: function (msg) {
            $("#AvailableElectrodeList option").remove();
            $("#SelectedElectrodeList option").remove();
            var a = msg.split(";");
            for (i = 0; i < a.length; i++) {
                
                $("#AvailableElectrodeList").append($("<option/>", {
                    text: a[i],
                    value:a[i]
                }))
            }            
        }

    })
}

function LoadCADDetail(ID) {
    $.ajax({
        url: "/Task/JsonCADDetail?TaskID=" + ID,
        type: "Get",
        dataType: "html",
        success: function (msg) {
            $("#ItemList option").remove();
            var a = msg.split(";");
            for (i = 0; i < a.length; i++) {

                $("#ItemList").append($("<option/>", {
                    text: a[i],
                    value: a[i],
                    selected:true
                }))
            }
        }

    })
}

function LoadMachine() {
    $("#Device option").remove();
    $.getJSON("/Task/CNCMachines", function (msg) {
        $.each(msg, function (i, n) {
            $("#Device").append($("<option/>", {
                value: n.MachineID,
                text: n.Name
            }))
        })
    })
}

function LoadElectrode(ID) {
    var _id;
    if (ID == undefined) {
        _id = GetCurrentID("ElectrodeGrid");
    } else {
        _id = ID;
    }
        
    var _title = GetCellContent("ElectrodeGrid", "LabelName");
    $("#EleCompensationLabel").html(_title);
    $("#LabelName").val(_title);
    $("#CNCItemID").val(_id);
    var _z = GetCellContent("ElectrodeGrid", "ZCompensation");
    var _gap = GetCellContent("ElectrodeGrid", "GapCompensation");
    $("#ZCompensation").val(_z);
    $("#GapCompensation").val(_gap);

    var _url = "/Task/JsonQCReport?CNCItemID=" + _id
    $.ajax({
        url: _url,
        type: "Get",
        dataType: "html",
        success: function (msg) {
            if (msg == "") {
                alert("未找到检测报告");
            } else {
                $("#ReportPic").attr("src", msg);
                $("#ReportLink").attr("href", msg);
            }
        }

    })



    $("#EleCompensationDialog").modal("show");

}


function SaveCNCItemResult() {
    var _url = "/Task/ElectrodeConfirm?CNCItemID=" + $("#CNCItemID").val() +
        "&Result=" + $("#Result").val() + "&ZCompensation=" + $("#ZCompensation").val() +
        "&GapCompensation=" + $("#GapCompensation").val();
    $.ajax({
        url: _url,
        type: "Get",
        dataType: "html",
        success: function (msg) {
            alert("已保存");
            $("#EleCompensationDialog").modal("hide");
            location.reload();
        }
    })
}


function CloseTask() {

    
        var _ids = GetMultiSelectedIDs("TaskGrid");
        if (_ids != "") {

            if ($("#CurrentTaskType").val() != 2) {
                var _url = "/Task/CloseTask?TaskIDs=" + _ids;
                $.ajax({
                    url: _url,
                    type: "Get",
                    dataType: "html",
                    success: function (msg) {
                        if (msg == "") {
                            alert("任务结束成功");
                        } else {
                            alert("以下任务结束失败，请重试:" + msg);
                        }

                        location.reload();
                    }

                })
            } else {
                var _id = _ids.split(",");
                CloseEDMTask(_id[0]);
            }
        } else {
            alert("请至少选择一项任务");
        }
    
}

function CloseEDMTask(TaskID) {
    LoadEDMTaskItems(TaskID);
    LoadNextDepartments();
    LoadEDMUsers();
    
    $("#FinishEDMDialog").modal("show");
}


function LoadEDMTaskItems(TaskID) {
    $("#EDMTaskID").val(TaskID);

 
    $("#ELEList p").remove();
    $("#ItemList p").remove();


    var _url = "/task/edmelelist?taskid=" + TaskID;
    var _eleListHeight = 200;
    var _itemListHeight = 200;
    $.getJSON(_url, function (msg) {
        $.each(msg, function (i, n) {
            $("#ELEList").append($('<p><input type="checkbox" id="' + n + '"/>' + n + "</p>"));
        })
        _eleListHeight = $("#ELEList").height();
    })

    _url = "/task/edmitemlist?taskid=" + TaskID;    
    $.getJSON(_url, function (msg) {
        $.each(msg, function (i, n) {
            $("#ItemList").append($('<p><input type="checkbox" id="' + n + '"/>' + n + "</p>"));
        })
        _itemListHeight = $("#ItemList").height();

        //if (_eleListHeight > _itemListHeight) {
        //    $("#ItemList").height(_eleListHeight);
        //} else {
        //    $("#ELEList").height(_itemListHeight);
        //}
        
    })
}

function LoadNextDepartments() {
    var _url = "/Task/NextDepartment?DepartmentID=8";
    $("#NextDepartment option").remove();
    $.getJSON(_url, function (msg) {
        $.each(msg, function (i, n) {
            //$("#NextDepartment").append($("<option/>", { value: n.DepartmentID, text: Name }));
            $("#NextDepartment").append($("<option/>", { value: n.DepartmentID, text: n.Name }));
        })
    })
}

function LoadEDMUsers() {
    var _url = "/Task/JsonUsers?DepartmentID=8";
    $("#Operator option").remove();
    $.getJSON(_url, function (msg) {
        $.each(msg, function (i, n) {
            $("#Operator").append($("<option/>", { value: n.UserID, text: n.FullName }));
        })
    })
}

function DownloadFile() {
    
    //var _url = "/Task/DownloadProgram";
    //$.ajax({
    //    url: _url,
    //    type: "Post",
    //    dataType: "html",
    //    data: $("#Download").serialize(),
    //    success: function (msg) {
    //        location.reload();
    //    }

    //})
    $("#Download").submit();
}


function GetProjectID(MoldNumber) {
    $.ajax({
        url: "/Project/GetProjectID?MoldNumber=" + MoldNumber,
        type: "Get",
        dataType: "html",
        success: function (msg) {
            $("#ProjectID").val(msg);
        }
    })
}

function LoadMoldList(CAM, State,TaskType, Keyword, CurrentVal) {
    var _url = "";
    console.log("CAM=" + CAM + "State=" + State + "TaskType=" + TaskType + "Keyword=" + Keyword + "CurrentVal=" + CurrentVal);

    $("#MoldSelect option").remove();
    if (CAM == 1) {
        if (State< 0) {
            _url = "/Task/GetMoldNumberList?State="+State+"&CAM=1&TaskType="+TaskType+"&Keyword="+Keyword;
        } else {
            _url = "/Task/GetMoldNumberList?State=1&CAM=1&TaskType=" + TaskType + "&Keyword=" + Keyword;
        }
    } else {
        if (State == 0) {
            _url = "/Task/GetMoldNumberList?State=0&CAM=0&TaskType=" + TaskType + "&Keyword=" + Keyword;
        } else {
            _url = "/Task/GetMoldNumberList?State=1&CAM=0&TaskType=" + TaskType + "&Keyword=" + Keyword;
        }
    }
    $.getJSON(_url, function (msg) {
        $.each(msg, function (i, n) {
            var _val = $.trim(n);
            if (_val != "") {
                if (CurrentVal == _val) {
                    $("#MoldSelect").append($("<option/>", {
                        text: _val,
                        value: _val,
                        selected:true
                    }))
                } else {
                    $("#MoldSelect").append($("<option/>", {
                        text: _val,
                        value: _val
                    }))
                }
               
            }
        })
    })
}

function CreateNewTask(_ids) {
    var _url = "/Task/CreateNewTaskByCAM?TaskIDs=" + _ids;

    $.ajax({
        url: _url,
        type: "Get",
        dataType: "html",
        success: function (msg) {
            if (msg == "") {
                alert("任务创建成功");
            } else {
                alert("msg");
            }
        }
    })
}

function QueryTaskByKeyword() {
    if ($("#TaskKeyword").val() != "") {
        RefreshTaskGrid($("#TaskKeyword").val());
    } else {
        alert("请输入查询任务关键字");
    }
}

function ConfirmFinishEDM() {
    var _url = "/Task/CloseTask?TaskIDs=" + $("#EDMTaskID").val() + "&Memo=" + $("#FinishMemo").val()+"&UserID="+$("#Operator").val();
    $.ajax({
        url: _url,
        type: "Get",
        success: function (msg) {
            if (msg == "") {
                alert("任务结束成功");
                $("#FinishEDMDialog").modal("hide");
                RefreshTaskGrid("");
            } else {
                alert("任务结束失败，请重试");
            }            
        }
    })
}

function ShowElePDF(taskid) {
    if (taskid == undefined) {
        alert("请选择电极加工任务");
    } else {
        var _url = "/Task/GetTaskPDF?TaskID="+taskid
        $.ajax({
            url: _url,
            type: "Get",
            success: function (msg) {
                window.open(msg);
            }
        })
    }

}
