//Scripts for Project edit

$(document).ready(function () {
    
    
    $("#CreateProject").on("click", function () {
        location.href = "/Project/Edit";
    })

    
    //Submit the project edit from when click the SaveProject button
    var usertarget;
    $("#SaveProject, #SaveProject1").on("click", function () {

        if ($("#Project\\.ProjectID").val() == 0) {
            

            $("#Project\\.Project\\.CustomerName").val($("#Project\\.Project\\.CustomerID option:selected").text());
        }
        
        if (ValidateCreate()) {
            $("#SaveProject").attr("disabled", "true");
            $("#SaveProject1").attr("disabled", "true");
            $("#Project\\.Project\\.CustomerID").removeAttr("disabled");
            //alert($("#ProjectEdit").serialize());
            $("#ProjectEdit").submit();
            return true;
        } else {
            return false;
        }
    });

    $("#MoldNumber").on("blur", function () {
        
            CheckMoldNumer();

    });

    $("#ProjectNumber").on("blur", function () {
        CheckProjectNumber();
    })

    //$("#SaveProject1").on("click", function () {
    //    $("#SaveProject").attr("disabled", "true");
    //    $("#SaveProject1").attr("disabled", "true");
    //    CheckMoldNumer();
    //    $("#Project\\.Project\\.CustomerName").val($("#Project\\.Project\\.CustomerID option:selected").text());

    //    if (ValidateCreate()) {

    //        $("#Project\\.Project\\.CustomerID").removeAttr("disabled");
    //        //alert($("#ProjectEdit").serialize());
    //        $("#ProjectEdit").submit();
    //        return true;
    //    } else {
    //        return false;
    //    }
    //});

    $(".userselect").on("click", function (event) {
        usertarget = "#" + this.id;
        LoadUsers("");
        
        $("#UserKeyword").val("");
        $("#UserSelect").modal("show");
    });

    //Filter the user list when user keyword is input
    $("#UserKeyword").on("keyup", function () {
        LoadUsers($("#UserKeyword").val());
    });

    //Define the selected user to project role
    $("#DefineUser").on("click", function () {
        SelectUser(usertarget);
    })

    $("#UserList").on("dblclick", function () {
        SelectUser(usertarget);
    })

    $("#PhaseID").on("change", function () {
        LoadPhase(1);
    });

    

    

    $("#SaveMemo").on("click", function () {
        $("#MemoEdit").submit();
    });


    $("#PlanCFinish").on("blur", function () {
        $("#PlanCFinish").removeClass("invalidefield");
        ValidatePhaseModify();
    })



    $("#PauseProjectBtn").on("click", function () {
        PauseProject();
    })

    $("#SearchProject").on("click", function () {
        $("#ProjectSearchDialog").modal("show");
    })

    $("#ProjectSearchBtn").on("click", function(){        
        location.href = "/Project/Index?keyword=" + $("#keyword").val() + "&state=" + $("#state").val() + "&type=" + $("#Type").val();
    })

    $("#AllProject").on("click", function () {
        location.href = "/Project/Index";
    })

    $("#DeleteProject").on("click", function () {
        
        $("#DeleteProjectID").val(GetCurrentID("ProjectGrid"));
        $("#DeleteProjectDialog").modal("show");
    })

    $("#DeleteProjectBtn").on("click", function () {
        DeleteProject();
    });
    $("#FinishPhaseModify").on("click", function () {
        FinishPhaseModify();
    });

    $("#CustomerList").on("change", function () {
        LoadCustomerInfo($("#CustomerList option:selected").val());
    })

    $("#SaveCustomer").on("click", function () {
        if (ValidateForm("SaveCustomerForm")) {
            SaveCustomer();
        }
    })

    $("#DeleteCustomer").on("click", function () {
        if ($("#CustomerList").val() != null) {
            if (confirm("确认要删除客户？")) {
                DeleteCustomer($("#CustomerList").val());
            }
        } else {
            alert("请先选择一个客户");
        }
    })

    $("#CreateCustomer").on("click", function () {
        $("#CustomerID").val(0);
        $("#Name").val("");
        $("#Address").val("");
        $("#Enabled").val(true);
        $("#Name").focusin();
    })

    $("#FinishPhaseID").on("change", function () {
        alert("只能结束本部门相关的项目阶段");
        $("#FinishPhaseID").val($("#FinishPhaseIDOriginal").val());
    })


    $("#ProjectFileSaveBtn").on("click", function () {
        var formData = new FormData($("#FileUpload")[0]);
        $.ajax({
            url: '/Project/SaveProjectFile',
            type: 'POST',
            data: formData,
            async: false,
            cache: false,
            contentType: false,
            processData: false,
            success: function (returndata) {
                alert(returndata);
                location.reload();
            },
            error: function (returndata) {
                alert(returndata);
            }
        });
        
        //SaveProjectFile();
    })

    $("#FinishedProject").on("click", function () {
        location = "/Project/Index?State=2";
    })

    $("#MoldProject").on("click", function(){
        location.href = "/Project/Index"
    })

    $("#FixMoldProject").on("click", function () {
        location.href = "/Project/Index?Type=2";
    })
});



//----------------------Project Edit Functions---------------------------------------------
//Load User information to UserList
//When keyword is changed, filter the users list according to the keywords
function LoadUsers(keyword) {
    var ajaxUsers = "/User/FilterUser";
    $("#UserList option").remove();    
    if (keyword != "") {
        ajaxUsers = ajaxUsers + "?UserName=" + keyword;
    }    
    $.getJSON(ajaxUsers, function (msg) {
        $.each(msg, function (i, n) {
            $("#UserList").append($("<option/>", {
                value: n.UserID,
                text:n.FullName
            }))
        })
    });
}

//when double click or close the user select modal
//close the modal and setup the target field value
function SelectUser(target) {
    if ($("#UserList option:selected").text() != "") {
        $(UnifyName(target)).val($("#UserList option:selected").text());
        $(GetTargetRoleIDField(target)).val($("#UserList option:selected").val());
        $("#UserSelect").modal("hide");
    } else {
        alert("请选择人员");
    }
}

//Get the user role id field name
function GetTargetRoleIDField(target) {
    return UnifyName(target.substring(0, target.lastIndexOf(".") + 1) + "UserID");    
}

//Load Customer information to dropdown list Project.CustomerID
function LoadCustomer(id) {
    
    $.getJSON("/Project/JsonCustomers", function (msg) {
        
        $.each(msg, function (i, n) {
            $("#Project\\.Project\\.CustomerID").append($("<option/>", {
                value: n.CustomerID,
                text: n.Name
            }))
        });
        if (id>0){
            $("#Project\\.Project\\.CustomerID").val(id);
        }
        //alert($("#Project\\.Project\\.CustomerID option").length());
    });      
}

//Validate the fields when creating project
function ValidateCreate() {
    var RequiredFieldValid = true;
    var PhaseDateValid = true;
    var errorMessage = "";
    //Required field is filled
    $("input.required").each(function () {
        var item = $("#" + UnifyName(this.id));
        if (item.val() == "") {
            item.addClass("invalidefield");
            RequiredFieldValid = false;
        } else {
            item.removeClass("invalidefield");
        }
    });
    errorMessage = "请填写以下必填项";

    if (!RequiredFieldValid) {
        $("#ErrorMessage").html(errorMessage);
        $("#ErrorMessage").removeClass("HiddenMessage");
    }
    return RequiredFieldValid;
}

function ValidateForm(FormName) {
    var RequiredFieldValid = true;
    var PhaseDateValid = true;
    var errorMessage = "";
    //Required field is filled
    $("#"+FormName+" input.required").each(function () {
        var item = $("#" + UnifyName(this.id));
        if (item.val() == "") {
            item.addClass("invalidefield");
            RequiredFieldValid= false;
        } else {
            item.removeClass("invalidefield");
        }
    });
    return RequiredFieldValid;
    
}



//Load the current project phase plan date to field
function LoadPhase(type) {
    var projectID = $("#ProjectID").val();
    var i = Number($("#selPhase").val());
    if (type==0){
        if (isNaN(i)) {
            $("#PhaseID").val(1);
        } else {
            $("#PhaseID").val($("#selPhase").val());
        }
    } 
    

    var phaseID = $("#PhaseID").val();
    var ajaxData = "/Project/JsonProjectPhase?ProjectID=" + projectID + "&PhaseID=" + phaseID;
    $("#PlanCFinish").removeClass("invalidefield");
    $.getJSON(ajaxData, function (msg) {
        if (msg.ProjectPhase!="0001-01-01"){
            $("#PlanFinish").val(msg.ProjectPhase);
        }
        if (msg.MainPhase == null) {
            $("#MainRow").addClass("HiddenMessage");
            $("#MainFinish").val("");
        } else {
            $("#MainRow").removeClass("HiddenMessage");
            if (msg.MainPhase!='0001-01-01'){
                $("#MainFinish").val(msg.MainPhase);
            }
        }
    });


}

//Shows the modify phase modal
function ShowPhaseModification(ProjectID) {
    $("#ProjectID").val(ProjectID);
    GetReasons();

    var $item = $("#PhaseID");
    LoadPhase(0);
    $("#ModifyMilestone").modal("show");
}

function FinishPhase(ProjectID) {
    $("#FinishProjectID").val(ProjectID);
    $("#FinishPhaseID").val($("#selPhase").val());
    $("#FinishPhaseIDOriginal").val($("#selPhase").val());
    $.ajax({
        dataType: "html",
        url: "/Project/PhaseDeptValidate?PhaseID=" + $("#selPhase").val() + "&DepartmentID=" + dept,
        type: "Get",
        success: function (msg) {
            if (msg=="True") {
                $("#FinishPhaseDialog").modal("show");
            } else {
                alert("只能结束本部门相关的项目阶段");
            }
        }

    })
}

function FinishPhaseModify() {
    var form = $("#FinishPhaseForm");
    var name = $("#FinishPhaseID option:selected").text();
    $.ajax({
        dataType: "html",
        url: "/Project/FinishPhase",
        data: form.serialize(),
        error: function () { },
        success: function (msg) {
            switch (msg) {
                case "0":
                    alert("项目" + name + "阶段结束");
                    $("#FinishPhaseDialog").modal("hide");
                    $("#ProjectGrid").jqGrid().trigger("reloadGrid");
                    break;
                case "1":
                    alert("项目阶段结束出现错误，请稍后重试");
                    break;
                case "2":
                    alert("所有子项目结束前无法结束主项目");
            }
        }
    })
}

//Get the list of existing phases
function GetPhases(CtrName, ProjectType) {
    $("#"+CtrName+" option").remove();
    $.getJSON("/Project/JsonPhases", function (msg) {
        $.each(msg, function (i, n) {

            if ((ProjectType == 2) && (n.PhaseID == 12)) {
                $("#" + CtrName).append($("<option/>", {
                    value: n.PhaseID,
                    text: "试模"
                }))
            } else {
                $("#" + CtrName).append($("<option/>", {
                    value: n.PhaseID,
                    text: n.Name
                }))
            }
            
        })
    });
}

//The phase finish date cannot be later than the date of main project
function ValidatePhaseModify() {
    if ($("#MainFinish").val() != "") {
        if ($("#PlanCFinish").val()>$("#MainFinish").val()){
            alert("模具项目节点完成时间不能晚于主项目节点完成时间");
            $("#PlanCFinish").addClass("invalidefield");
        } else {
            $("#PlanCFinish").removeClass("invalidefield");
        }
    }
}

//Get the list of milestone modification reason
function GetReasons() {
    $("#Reason option").remove();
    $.getJSON("/List/ListValuesByName?TypeName=计划调整原因", function (msg) {
        $.each(msg, function (i, n) {
            $("#Reason").append($("<option/>", {
                text: n.Name
            }))
        })
    });
}


//----------------------Project Memo Edit Functions---------------------------------------------
//Get project memo information
function GetMemo(GridTableName) {
    var item = $("#" + GridTableName);
    var memo = item.getCell(item.getGridParam("selrow"), "Memo").replace(/<br>/g, "\r\n");
    return memo;
}


//----------------------Common Functions---------------------------------------------
//Convert Json format datetime value to 'yyyy-MM-dd' format
function DateToString(DateValue) {
    var planFinish = new Date(parseInt(DateValue.replace(/\D/igm, "")));
    var year = planFinish.getFullYear();
    var month = planFinish.getMonth() + 1;
    var date = planFinish.getDate();

    month = month > 9 ? month : "0" + month;
    date = date > 9 ? date : "0" + date;
    return year + "-" + month + "-" + date;
}

//Get current date in yyyy-MM-dd format
function GetNow() {
    var today = new Date();
    var year = today.getFullYear();
    var month = (today.getMonth() + 1) > 9 ? (today.getMonth() + 1) : "0" + (today.getMonth() + 1);
    var day = today.getDate() > 9 ? today.getDate() : "0" + today.getDate();
    return year+"-"+month+"-"+day;
}



//Get the sequence of field
//Parameter: 
//FieldName: name of the input field(eg:Project.ProjectPhase[4])
//Return:4
function GetFieldSequence(FieldName) {
    var start = FieldName.lastIndexOf("\[")+1;
    var end = FieldName.lastIndexOf("\]");
    var seq = FieldName.substring(start,end);
    return seq;
}

function ShowPauseProject(ProjectID) {
    $("#PauseProjectID").val(ProjectID);
    $.getJSON("/Project/JsonProject?ProjectID=" + ProjectID, function (msg) {
        
        $("#ParentID").val(msg.ParentID);
        var state = msg.ProjectStatus;
        if (state == 0) {
            $("#PauseProjectLabel").html("暂停");
        } else {
            $("#PauseProjectLabel").html("继续");
        }
    });
    $("#PauseProjectDialog").modal("show");
}

function PauseProject() {
    var _data ="ProjectID=" + $("#PauseProjectID").val() + "&Memo=" + $("#PauseMemo").val();
    if ($("#ParentID").val() == 0) {
        if (confirm("当前项目为主项目,是否需要同时"+$("#PauseProjectLabel").html()+"全部子项目?")) {
            _data = _data + "&PauseSubs=true"
        } else {
            _data = _data + "&PauseSubs=false"
        }
    } else {
        _data =_data + "&PauseSubs=false"
    }
    var _url = "/Project/PauseProject?"+_data;
    $.ajax({
        dataType: "html",
        url: _url,
        type:"GET",
        error: function () { },
        success: function (msg) {
            if (msg == 0) {
                alert("项目启动成功");
            } else {
                alert("项目暂停完成");
            }
            $("#PauseProjectDialog").modal("hide");
            $("#ProjectGrid").jqGrid().trigger("reloadGrid");
            $("#PauseMemo").val("")
        }

    })
}


function DeleteProject() {
    var _data;
    _data = "ProjectID=" + $("#DeleteProjectID").val() + "&Memo=" + $("#DeleteMemo").val();
    $("#DeleteProjectID").val()
    $.ajax({
        dataType: "html",
        url: "/Project/SubProjectCount?ProjectID=" + $("#DeleteProjectID").val(),
        error: function () { },
        success: function (msg) {
            if (msg == 0) {
                $.ajax({
                    dataType: "html",
                    url: "/Project/DeleteProject?" + _data,
                    error: function () { },
                    success: function (msg) {
                        alert("项目已删除");
                        $("#DeleteProjectDialog").modal("hide");
                        $("#ProjectGrid").jqGrid().trigger("reloadGrid");
                        $("#DeleteMemo").val("");
                    }
                })
            } else {
                alert("主项目下还有(新/修)模具项目，无法删除。请先删除全部子项目");
            }
        }
    })
}

function CheckMoldNumer() {
    if ($("#Project\\.ProjectID").val() == 0) {
        var moldNo = $("#MoldNumber").val();
        $.ajax({
            dataType: "html",
            url: "/Project/CheckMoldNumber?MoldNumber=" + moldNo,
            error: function () { },
            success: function (msg) {
                if (msg == 1) {
                    alert("模具号已存在！");
                    $("#MoldNumber").addClass("invalidefield");
                    $("#MoldNumber").val("");
                }
            }
        })
    }
}

function CheckProjectNumber() {
    var ProjectNo = $("#ProjectNumber").val();
    $.ajax({
        dataType: "html",
        url: "/Project/CheckProjectNumber?ProjectNumber=" + ProjectNo,
        error: function () { },
        success: function (msg) {
            if (msg == 1) {
                alert("项目号已存在！");
                $("#ProjectNumber").addClass("invalidefield");
                $("#ProjectNumber").val("");
            }
        }
    })
}

function LoadCustomerInfo(id) {
    $.getJSON("/Project/JsonCustomer?CustomerID=" + id, function (msg) {
        $("#Name").val(msg.Name);
        $("#Address").val(msg.Address);
        $("#Enabled").val(msg.Enabled);
        $("#CustomerID").val(msg.CustomerID);
    });
}

function DeleteCustomer(id) {
    $.ajax({
        dataType: "html",
        url: "/Project/DeleteCustomer?CustomerID=" + id,
        error: function () { },
        success: function (msg) {
            if (msg == "") {
                location.href = "/Project/CustomerManagement";
            } else {
                alert(msg);
            }
        }
    })
}

function SaveCustomer() {
    if ($("#CustomerID").val() > 0) {
        $("#SaveCustomerForm").submit();
    } else {
        $.ajax({
            type: "Get",
            dataType: "html",
            url: "/Project/UniqueCustomer?Name=" + $("#Name").val(),
            success: function (msg) {
                if (msg == 0) {
                    $("#SaveCustomerForm").submit();
                } else {
                    alert("客户已存在");
                }
            }
        })
    }
}

function SaveProjectFile() {
    var _url = "/Project/SaveProjectFile?ProjectID=" + $("#FileProjectID").val() + "&Attachment=" + $("#FilePath").val();

    $.ajax({
        type: "Get",
        dataType: "html",
        url: _url,
        success: function (msg) {
            alert("文件路径已保存");
        }
    })
}


function CreateMoldFixProject(row) {
    var _url = "/Project/CheckMainFOT?ProjectID=" + row;

    $.ajax({
        type: "Get",
        dataType: "html",
        url: _url,
        success: function (msg) {
            if (msg == "") {
                location.href = "/Project/Edit?ParentID=" + row + "&ProjectType=2";
            } else {
                alert(msg);
            }
        }
    })    
}

function ShowProjectHistory(row) {
    var _url = "/Project/JsonProjectRecord?ProjectID=" + row;
    $("#ProjectLog option").remove();
    $.getJSON(_url, function (msg) {
        $.each(msg, function (i, n) {
            var _value = renderDate(n.RecordDate) + "    " + n.RecordContent;
            $("#ProjectLog").append($("<option/>", {
                text: _value
            }))
        })
    });
    $("#ProjectHistoryDialog").modal("show");
}


function GetMoldNumber(ProjectID) {
    var _url = "/Project/GetMoldNumber?ProjectID=" + ProjectID;
    $.ajax({
        type: "Get",
        url: _url,
        success: function (msg) {

        }
    })
}

function PhaseTask(ProjectID) {
    var _url = "/Project/GetMoldNumber?ProjectID=" + ProjectID;
    TaskList("", -1, -1, -1);
    $.ajax({
        type: "Get",
        url: _url,
        success: function (msg) {
            $("#MoldNumber").val(msg);
            $("#State").val(0);
            var _moldNumber = $("#MoldNumber").val();
            var _phaseID = Number($("#selPhase").val());
            if (_moldNumber != '---') {
                switch (_phaseID) {
                    case 3:
                        //采购
                        break;
                    case 4:
                        //开粗
                        alert("本阶段不包含加工任务");
                        break;
                    case 5:
                        //CNC开粗
                        alert("本阶段不包含加工任务");
                        break;
                    case 6:
                        //热处理
                        alert("本阶段不包含加工任务");
                        break;
                    case 7:
                        //磨床
                        alert("本阶段不包含加工任务");
                        break;
                    case 8:
                        //CNC
                        $("#CNCType").attr("style", "display:solid")
                        $("#TaskType").val(1);
                        var _url = "/Task/JsonMachineTasks?MoldNumber=" + _moldNumber + "&TaskType=1&State=0";
                        $("#TaskGrid").jqGrid('setGridParam', { datatype: "json", url: _url }).trigger("reloadGrid");
                        $("#PhaseTaskDialog").attr("style", _width = document.body.clientWidth * 0.6 + 100);
                        $("#PhaseTaskDialog").modal("show");
                        break;
                    case 9:
                        //EDM
                        $("#CNCType").attr("style", "display:none")
                        $("#TaskType").val(2);
                        var _url = "/Task/JsonMachineTasks?MoldNumber=" + _moldNumber + "&TaskType=2&State=0";
                        $("#TaskGrid").jqGrid('setGridParam', { datatype: "json", url: _url }).trigger("reloadGrid");
                        $("#PhaseTaskDialog").attr("style", _width = document.body.clientWidth * 0.6 + 100);
                        $("#PhaseTaskDialog").modal("show");
                        break;
                    case 10:
                        //WEDM
                        $("#CNCType").attr("style", "display:none")
                        $("#TaskType").val(3);
                        var _url = "/Task/JsonMachineTasks?MoldNumber=" + _moldNumber + "&TaskType=3&State=0";
                        $("#TaskGrid").jqGrid('setGridParam', { datatype: "json", url: _url }).trigger("reloadGrid");
                        $("#PhaseTaskDialog").attr("style", _width = document.body.clientWidth * 0.6 + 100);
                        $("#PhaseTaskDialog").modal("show");
                        $("#PhaseTaskDialog").modal("show");
                        break;
                    case 11:
                        //装配
                        alert("本阶段不包含加工任务");
                        break;
                    default:
                        alert("本阶段不包含加工任务");
                        break;
                }
            } else {
                alert("主项目不包含加工任务")
            }
            
        } 
    })
    
    
}

function CellEdit_ModifyProJPhase(ProjectID, PhaseID, PlanCFinish,flag) {
    var result;
    $.ajaxSettings.async = false;//同步请求
    $.get('/Project/Service_Save_ProJPhaseCFDate?ProJID=' + ProjectID + '&PhaseID=' + PhaseID + '&CFDate=' + PlanCFinish + '&Flag=' + flag, function (res) {
        //res = JSON.parse(res);
        result = res.Code;
    });
    return result;
}