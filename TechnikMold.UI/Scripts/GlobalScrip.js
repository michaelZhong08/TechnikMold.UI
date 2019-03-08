function LoadUserRoles(depid,posid) {
    var _url = "/User/JsonUserRoleList?UserID=" + $("#CurrentUserID").val();
    $("#UserRoles option").remove();
    $.getJSON(_url, function (msg) {
        $.each(msg, function (i, n) {
            var _html = '';
            if (n.DepID == depid && n.Position == posid) {
                _html = $("<option/>", { value: n.UserRoleID, text: n.DisplayName, selected: true });
            } else {
                _html = $("<option/>", { value: n.UserRoleID, text: n.DisplayName });
            }
            $("#UserRoles").append(_html);
        })
    })
}

function SwitchRole() {
    var _url = "/User/ReloadCookie?UserRoleID=" + $("#UserRoles").val();
    $.ajax({
        type: "Get",
        url: _url,
        success: function () {
            location.reload();
        }
    })
}

//页面加载延迟时提醒用户等待脚本

//获取浏览器页面可见高度和宽度
var _PageHeight = document.documentElement.clientHeight,
    _PageWidth = document.documentElement.clientWidth;
//计算loading框距离顶部和左部的距离（loading框的宽度为215px，高度为61px）
var _LoadingTop = _PageHeight > 61 ? (_PageHeight - 61) / 2 : 0,
    _LoadingLeft = _PageWidth > 215 ? (_PageWidth) / 2 : 0;
//在页面未加载完毕之前显示的loading Html自定义内容
var _LoadingHtml = '<div id="loadingDiv" style="position:absolute;left:0;width:100%;height:' + _PageHeight + 'px;top:0;background:#ffffff;opacity:0.8;filter:alpha(opacity=80);z-index:10000;"><div style="position: absolute; cursor1: wait; left: ' + _LoadingLeft + 'px; top:' + _LoadingTop + 'px; width: auto; height: 200px; padding-left: 150px; background: #fff url(/Images/loading-1.gif) no-repeat ;"></div></div>';
//呈现loading效果
document.write(_LoadingHtml);
//监听加载状态改变
document.onreadystatechange = completeLoading;
//加载状态为complete时移除loading效果
function completeLoading() {
    if (document.readyState == "complete") {
        var loadingMask = document.getElementById('loadingDiv');
        loadingMask.parentNode.removeChild(loadingMask);
    }
}

function B_forbiden_menu() { //禁用鼠标右键
    window.oncontextmenu = function () {
        return false;
    }
}

/* Switch 脚本 */
function GetSwitchVal(divid) {
    var _cusFlagID = divid + '_swFlag';
    return $('#' + _cusFlagID).val();
}

function CusSwitchConfig(e, divid, lLabel, rLabel, initailState, lColer, rColer) {
    if (initailState == null || initailState == undefined) {
        initailState = true;
    }
    var _cusCls = divid + '-switch-toggle-container';
    var _cusFlagID = divid + '_swFlag';
    var _swhtml = '<input id=' + _cusFlagID + ' value=' + initailState.toString() + ' hidden/><div class="switch-father-container ' + divid + '">';
    _swhtml += '<div class="switch-inner-container">';
    _swhtml += '<div class="switch-toggle"><p>' + rLabel + '</p></div><div class="switch-toggle"><p>' + lLabel + '</p></div>';
    _swhtml += '</div>';
    _swhtml += '<div class="switch-inner-container ' + _cusCls + '">';
    _swhtml += '<div class="switch-toggle"><p>' + rLabel + '</p></div><div class="switch-toggle"><p>' + lLabel + '</p></div>';
    _swhtml += '</div>';
    _swhtml += '</div>';

    $('#' + divid).append(_swhtml);
    //状态初始化
    var toggle = document.getElementsByClassName(divid)[0];
    var toggleContainer = document.getElementsByClassName(_cusCls)[0];
    var toggleNumber = initailState;
    if (initailState) {
        toggleContainer.style.clipPath = 'inset(0 50% 0 0)';
        toggleContainer.style.backgroundColor = 'dodgerblue';
    } else {
        toggleContainer.style.clipPath = 'inset(0 0 0 50%)';
        toggleContainer.style.backgroundColor = '#D74046';
    }

    if (lColer == null || lColer == undefined) {
        lColer = 'dodgerblue'
    }
    if (rColer == null || rColer == undefined) {
        rColer = '#D74046'
    }
    toggle.addEventListener('click', function () {
        toggleNumber = !toggleNumber;
        if (toggleNumber) {
            toggleContainer.style.clipPath = 'inset(0 50% 0 0)';
            toggleContainer.style.backgroundColor = 'dodgerblue';
        } else {
            toggleContainer.style.clipPath = 'inset(0 0 0 50%)';
            toggleContainer.style.backgroundColor = '#D74046';
        }
        $('#' + _cusFlagID).val(toggleNumber.toString());
        if (e != null) {
            e(GetSwitchVal(divid));
        }
    });
}
/**/