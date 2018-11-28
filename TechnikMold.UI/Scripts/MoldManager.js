$("document").ready(function () {
    //NoUser();

    $("#exit").on("click", function () {
        Logout();
    })
    //设置系统特征值
    //hr / sinno
    sessionStorage.setItem('SpecKey', 'sinno');
    SysSpecShow(sessionStorage['SpecKey']);
})

function GetCookie(CookieName) {
    var value = document.cookie;
    var head = "User=";
    value = value.substring(head.length);
    var arrCookie = value.split("&");
    for (var i = 0; i < arrCookie.length; i++) {
        var arr = arrCookie[i].split("=");
        if (arr[0] == CookieName) {
            return arr[1];
        }
    }
    return "";
}

function GetUserID() {
    var name = "UserID";
    return GetCookie(name);
}

function GetFullName(){
    var name="FullName";
    return GetCookie(name);
}

function GetDepartment() {
    var name = "Department";
    return GetCookie(name);
}

//Load the materials to dropdown list in dialog
function LoadMaterials(MaterialID, AddEmpty) {
    $("#MaterialID option").remove();
    $.getJSON("/Part/JsonMaterials", function (msg) {
        if (AddEmpty != 1) {
            $("#MaterialID").append($("<option/>", { value: "0", text: "-" }));
        }
        
        $.each(msg, function (i, n) {
            if (MaterialID == n.MaterialID) {
                $("#MaterialID").append($("<option/>", {
                    value: n.MaterialID,
                    text: n.Name,
                    selected:true
                }))
            } else {
                $("#MaterialID").append($("<option/>", {
                    value: n.MaterialID,
                    text: n.Name
                }))
            }
            
        })
    });
}

function LoadHardness(MaterialID, Hardness, AddEmpty) {
    var ajax = "/Part/JsonHardness?MaterialID=" + MaterialID;
    $("#HardnessID option").remove();
    $.getJSON(ajax, function (msg) {
        
        $("#HardnessID").append($("<option/>", { value: "0", text: "-" }));

        

        $.each(msg, function (i, n) {
            
            if (Hardness == n.Value) {
                $("#HardnessID").append($("<option/>", {
                    value: n.HardnessID,
                    text: n.Value,
                    selected: true
                }))
            } else {
                $("#HardnessID").append($("<option/>", {
                    value: n.HardnessID,
                    text: n.Value
                }))
            }
            
        })
        
    })
}

function LoadHardnessByName(MaterialName, Hardness) {
    var ajax = "/Part/JsonHardnessByName?MaterialName=" + MaterialName;
    $("#HardnessID option").remove();
    $.getJSON(ajax, function (msg) {
        $("#HardnessID").append($("<option/>", { value: "0", text: "-" }));
        $.each(msg, function (i, n) {
            if (Hardness == n.Value) {
                $("#HardnessID").append($("<option/>", {
                    value: n.HardnessID,
                    text: n.Value,
                    selected: true
                }))
            } else {
                $("#HardnessID").append($("<option/>", {
                    value: n.HardnessID,
                    text: n.Value
                }))
            }
        })
        
    })
}

function LoadUserName(UserID) {
    $.getJSON("/User/GetUserByID?UserID=" + UserID, function (msg) {
        return msg.FullName;
    })
}

//Replace reserved characters contained in field names
//.=>\.
//[=>\[
//]=>\]
function UnifyName(name) {
    var e = new RegExp("\\.", "g");
    value = name.replace(e, "\\.");
    e = new RegExp("\\[", "g");
    value = value.replace(e, "\\[");
    e = new RegExp("\\]", "g");
    value = value.replace(e, "\\]");
    return value;
}

function renderDate(date) {
    var da = new Date(parseInt(date.replace("/Date(", "").replace(")/", "").split("+")[0]));
    var year = da.getFullYear();
    var month = da.getMonth()+1;
    var day = da.getDate();
    month = month > 9 ? month : "0" + month;
    day = day >9?day: "0"+day;
    //return da.getFullYear() + "-" + (da.getMonth() + 1) + "-" + da.getDate();
    return year + "-" + month + "-" + day;
}

function renderTime(date) {
    var da = new Date(parseInt(date.replace("/Date(", "").replace(")/", "").split("+")[0]));
    var year = da.getFullYear();
    var _month = da.getMonth()+1;
    var month = _month > 9 ? _month : "0" + _month;
    var day = da.getDate() > 9 ? da.getDate() : "0" + da.getDate();
    var hour = da.getHours() > 9 ? da.getHours() : "0" + da.getHours();
    var minute = da.getMinutes() > 9 ? da.getMinutes() : "0" + da.getMinutes();
    return year + "-" + month + "-" + day + " " + hour + ":" + minute;
}

function ValidateEmail(EmailField) {
    var email_address = $("#" + EmailField).val();
    var regex = /^([0-9A-Za-z\-_\.]+)@([-0-9a-z]+\.[a-z]{2,3}(\.[a-z]{2})?)$/g;
    if (regex.test(email_address)) {
        return true;
    }
    else {
        alert("您输入的电子邮件地址不合法");
        $("#"+EmailField).addClass("invalidefield");
        return false;
    }
}

function ValidatePositive(FieldName) {
    var item = $("#" + FieldName);
    var value = Number(item.val());
    if (value >= 0) {
        item.removeClass("invalidefield");
        return true;
    } else {
        alert("输入值不能小于0")
        item.addClass("invalidefield");
        return false;
    }
}

function GetInvalidCount(FormName) {
    var item = $("#" + FormName + " .invalidefield");
    return item.length;
}


function Logout() {
    $.ajax({
        url: "/User/ClearCookie",
        type: "Get",
        success: function () {
            alert("当前用户已退出登录");
            location.reload();
        }
    })
}

function SysSpecHide(spec) {
    $('.' + spec).hide();
}

function SysSpecShow(spec) {
    $('.' + spec).show();
}

function B_forbiden_menu() { //禁用鼠标右键
    window.oncontextmenu = function () {
        return false;
    }
}