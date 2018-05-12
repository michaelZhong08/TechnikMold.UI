$("document").ready(function () {
    $("#QueryUser").on("click", function () {
        ShowQuery();
    })

    $("#QueryUserBtn").on("click", function () {
        location.href = "/User/Index?Keyword=" + $("#UserKeyword").val();
    })


    $("#DeleteUser").on("click", function () {
        DeleteUsers();
    })

    $("#SaveUser").on("click", function () {
        $("#DepartmentID").val($("#Department option:selected").val());
        $("#PositionID").val($("#Position").val());
        if (GetInvalidCount("UserEdit") == 0) {

            if (ValidateCreate("UserEdit")) {
                if (ValidateEmail("Email")) {                    
                    $("#UserEdit").submit(); 
                } else {
                    return false;
                }
            } else {
                return false;
            }
        } else {
            return false;
        }
    })

    $("#CreateUser").on("click", function () {
        NewUser();
    })

    $("#LogonName").on("blur", function(){
        if ($("#UserID").val() == 0) {
            ValidateUserExist("LogonName");
        }
    })
})

function ShowQuery() {
    $("#QueryUserModal").modal("show");
}


//$("#DeleteUsers").on("click", function () {
//    var selrows = $("#PRContentGrid").jqGrid('getGridParam', 'selarrrow');

//    while (selrows.length > 0) {
//        var _id = $("#PRContentGrid").getCell(selrows[0], "ID");
//        if (_id > 0) {
//            $.ajax({
//                dataType: "html",
//                url: "/Purchase/DeletePRContent?PRContentID=" + _id,
//                error: function () {

//                },
//                success: function (msg) {

//                }
//            });
//        }
//        $("#PRContentGrid").delRowData(selrows[0]);
//    }
//})

function DeleteUsers() {
    var selrows = $("#UserGrid").jqGrid("getGridParam", "selarrrow");
    if (selrows.length>0){
        var _ids = "";
        for (var i = 0; i < selrows.length;i++) {
            _ids = _ids + $("#UserGrid").getCell(selrows[i], "ID") + ",";
        }
        if (confirm("确认删除选中用户?")) {
            location.href = "/User/DeleteUser?UserIDs=" + _ids;
        }
    } else {
        alert("请选择至少一个用户");
    }
}

//Setup the UserEdit modal fields by user logon name
function LoadUser(id) {
    $.getJSON("/User/GetUserByID?UserID=" + id, function (user) {
        $("#LogonName").val(user.LogonName);
        $("#UserID").val(user.UserID);
        $("#DepartmentID").val(user.DepartmentID);
        $("#Enabled").val(user.Enabled);
        $("#FullName").val(user.FullName);
        $("#Email").val(user.Email);
        $("#Extension").val(user.Extension);
        $("#Mobile").val(user.Mobile);
        $("#Department").find("option[value=" + user.DepartmentID + "]").attr("selected", true);
        $("#Position").find("option[value=" + user.PositionID + "]").attr("selected", true);
        $("#EditUserModal").modal("show");
    })
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

function LoadDepartment() {
    $.getJSON("/User/departments", function (msg) {
        $.each(msg, function (i, n) {
            $("#Department").append($("<option/>", {
                value: n.DepartmentID,
                text: n.Name
            }))
            $("#RDepartmentID").append($("<option/>", {
                value: n.DepartmentID,
                text: n.Name
            }))
        });
    })
}

function LoadPositions() {
    $.getJSON("/User/JsonPositions", function (msg) {
        $.each(msg, function (i, n) {
            $("#Position").append($("<option/>", {
                value: n.PositionID,
                text: n.Name
            }))
            $("#RPositionID").append($("<option/>", {
                value: n.PositionID,
                text: n.Name
            }))
        });
    })
}

function ValidateUserExist(UserNameField) {
    var item = $("#" + UserNameField);    
    var url = "/User/ValidateUserExist?UserName=" + item.val();
    var result;
    $.ajax({
        dataType: "html",
        url: url,
        error: function () { },
        success: function (msg) {
            if (msg == 'False') {
                alert("用户名已存在");
                item.addClass("invalidefield")
            } else {
                item.removeClass("invalidefield");
            }
        }
    })
}
