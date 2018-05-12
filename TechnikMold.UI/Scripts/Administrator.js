/// <reference path="D:\Study\MoldManager\MoldManager.WebUI\Views/User/Index.cshtml" />
$(document).ready(
    function () {


        //-------------------------------列表相关开始-------------------------------------------
        //列表类型保存
        $("#SaveType").on("click", function () {
            $("#ListTypeEdit").submit();
        });

        //创建列表类型对话框初始化
        $("#AddListType").on("click", function () {
            EditListType(0, "");
        });

        //编辑列表类型对话框初始化
        $("#EditListType").on("click", function () {
            EditListType($("#ListType").val(), $("#ListType option:selected").text());
        })

        //选中列表类型时加载列表内容
        $("#ListType").on("change", function () {
            $("#ListContent option").remove();
            $("#ListContent").removeAttr("disabled");
            var typeID = $("#ListType option:selected").val();
            LoadList("/List/ListValues?TypeID=" + typeID, "ListContent");
            $("#ListContentEdit>#ListTypeID").val(typeID);
        });

        //编辑列表内容
        $("#EditListContent").on("click", function () {
            EditListContent($("#ListContent").val(), $("#ListContent option:selected").text())

        })
        //列表内容保存
        $("#SaveContent").on("click", function () {
            $("#ListContentEdit").submit();
        });

        $("#SaveCost").on("click", function () {
            $("#TaskHourCostEdit").submit();
        })

        //-------------------------------列表相关结束-------------------------------------------

        //-------------------------------部门相关开始-------------------------------------------

        $("#CreateDept").on("click", function () {
            $("#DepartmentID").val(0);
            $("#Name").val("");
            $("#Enabled").val("true");
            alert("请在右侧输入部门信息");
        })


        $("#DeleteDept").on("click", function () {
            if ($("#DepartmentList").val() != null) {
                if (confirm("确认要删除部门？")) {
                    DeleteDepartment($("#DepartmentList").val());
                }
            } else {
                alert("请先选择一个部门");
            }
        })

        $("#DepartmentList").on("change", function () {
            $("#DepartmentID").val($("#DepartmentList option:selected").val());
            $("#Name").val($("#DepartmentList option:selected").text());
            $("#Enabled").val("true");
        })

        $("#SaveDepartment").on("click", function () {
            if (ValidateCreate("SaveDepartmentForm")) {
                $("#SaveDepartmentForm").submit();               
            }
        })

        $("#SaveDepartmentForm #Name").on("blur", function () {
            var name = $("#SaveDepartmentForm #Name").val()
            ValidateDeptExist(name);

        })
        //-------------------------------部门相关结束-------------------------------------------

        //-------------------------------岗位相关开始-------------------------------------------

        $("#CreatePos").on("click", function () {
            $("#PositionID").val(0);
            $("#Name").val("");
            $("#Enabled").val("true");
            alert("请在右侧输入部门信息");
        })

        $("#DeletePos").on("click", function () {
            if ($("#PositionList").val() != null) {
                if (confirm("确认要删除岗位？")) {
                    DeletePosition($("#PositionList").val());
                }
            } else {
                alert("请先选择一个部门");
            }
        })

        $("#SavePos").on("click", function () {
            if (ValidateCreate("SavePositionForm")){
                $("#SavePositionForm").submit();
            } else {
                alert("请输入岗位名称");
            }
        })

        $("#PositionList").on("change", function () {
            $("#PositionID").val($("#PositionList option:selected").val());
            $("#Name").val($("#PositionList option:selected").text());
            $("#Enabled").val("true");
        })

        //-------------------------------岗位相关结束-------------------------------------------

    }
);

//加载列表内容
//ajax:获取json格式数据的方法
//control:列表内容加载目标控件
function LoadList(ajax, control) {
    $.getJSON(ajax, function (msg) {
        $.each(msg, function (i, n) {
            $("#" + control).append($("<option/>", {
                value: n.ListValueID,
                text: n.Name
            }))
        });
    })
}

//列表类型编辑对话框初始化
function EditListType(ID, Name) {
    $("#ListTypeEdit>#ListTypeID").val(ID);
    $("#ListTypeEdit>#Name").val(Name);
}

//function EditListContent(ID, Name) {
//    $("#ListContentEdit>#ListValueID").val(ID);
//    $("#ListContentEdit>#Name").val(Name);
//}





function NewUser() {   
    
    $("#LogonName").val("");
    $("#UserID").val("");
    $("#DepartmentID").val("");
    $("#Enabled").val("");
    $("#FullName").val("");
    $("#Email").val("");
    $("#Extension").val("");
    $("#Mobile").val("");
    $("#Enabled").val("true");
    $("#EditUserModal").modal("show");
}

function DeleteDepartment(id) {
    $.ajax({
        dataType: "html",
        url: "/Administrator/DeleteDepartment?DepartmentID=" + id,
        error: function () { },
        success: function (msg) {
            if (msg == "") {
                location.href = "/Administrator/Department";
            } else {
                alert(msg);
            }
        }
    })
}

function DeletePosition(id) {
    $.ajax({
        dataType: "html",
        url: "/Administrator/DeletePosition?PositionID=" + id,
        error: function () { },
        success: function (msg) {
            if (msg == "") {
                location.href = "/Administrator/Position";
            } else {
                alert(msg);
            }
        }
    })
}


function ValidateDeptExist(name) {
    if ($("#SaveDepartmentForm #DepartmentID").val() == 0) {
        $.ajax({
            dataType: "html",
            url: "/Administrator/ValidateDeptExist?DepartmentName=" + name,
            error: function () { },
            success: function (msg) {
                if (msg == "True") {
                    alert("部门已存在");
                    $("#SaveDepartmentForm #Name").addClass("invalidefield");
                } 
            }
        })
    }
}
