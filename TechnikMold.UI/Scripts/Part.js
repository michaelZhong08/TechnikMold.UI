$(document).ready(function () {

    LoadBrands();
    LoadMaterials();
    LoadSuppliers();
    LoadProject($("#ProjectID").val());

    //Show the change mold project dialog
    $("#ChangeProject").on("click", function () {
        ShowMoldSelect();
    });

    //$("#Keyword").on("keyup", function () {
    //    LoadMolds($("#Keyword").val());
    //})


    //Select the project and jump to the project part list
    //When double click the mold project from the project list
    //$("#MoldList").on("dblclick", function () {
    //    SelectProject();
    //});
    ////Select the project and jump to the project part list
    ////If no project is selected, do nothing
    //$("#SelectProject").on("click", function () {
    //    if ($("#MoldList option:selected").val() != undefined)
    //    {
    //        SelectProject();
    //    }
    //    //
    //});

    $("#CreatePart").on("click", function () {
        var _moldNumber = $("#MoldSelect").val();
        if (_moldNumber != null) {
            $("#MoldNumber").val(_moldNumber);
            $("#JobNo").val("");
            $("#ShortName").val("");
            //$("#PartNumber").val(_moldNumber + "-");
            //$("#PartName").val(_moldNumber + "_");
            $("#BomVersion").val($("#sltVersion").val());
            $("#PartID").val('0');
            $("#Specification").removeAttr("disabled", true);
            $("#MaterialID").removeAttr("disabled", true);
            $("#Quantity").removeAttr("disabled", true);
            $("#BrandID").removeAttr("disabled", true);

            document.getElementById('DetailDrawing').checked = false;
            document.getElementById('BriefDrawing').checked = false;
            document.getElementById('PurchaseDrawing').checked = false;
            document.getElementById('ExtraMaching').checked = false;
            $("#PartEditModal").modal("show");
        } else {
            alert("请先选择模具号");
        }

    });

    //Process the fields when part edit dialog shows
    $("#PartEditModal").on("shown.bs.modal", function () {
        if ($("#PartID").val() == 0) {
            $("#MaterialID option:first-child").attr("selected", "true");
            LoadHardness($("#MaterialID option:selected").val());
        }
        $("#PartNumber").removeClass("invalidefield");
        $("#PartNumber").val($("#MoldNumber").val() + "-");
        $("#Name").removeClass("invalidefield");
        $("#Name").val("");
        $("#Specification").removeClass("invalidefield");
        $("#Specification").val("");
        $("#Quantity").removeClass("invalidefield");
        $("#Quantity").val(1);
        $("#FromUG").html("");
        $("#SavePart").removeAttr("disabled");
        $("#MaterialID").val(0);
        $("#HardnessID").val(0);
        $("BrandID").val(0);
        $("#Memo").val("");
    })

    //Load the material hardness when changing the material
    $("#MaterialID").on("change", function () {
        LoadHardness(this.value);
    })
    $('#SPClose').on('click', function () {
        document.getElementById('DetailDrawing').checked = false;
        document.getElementById('BriefDrawing').checked = false;
        document.getElementById('PurchaseDrawing').checked = false;
        document.getElementById('ExtraMaching').checked = false;
    })
    $("#SavePart").on("click", function () {

        //alert($("#PartEdit").serialize());
        if (ValidateCreate()) {
            var DetailDrawing = document.getElementById('DetailDrawing');
            var BriefDrawing = document.getElementById('BriefDrawing');
            var PurchaseDrawing = document.getElementById('PurchaseDrawing');
            var ExtraMaching = document.getElementById('ExtraMaching');
            $("#MaterialName").val($("#MaterialID option:selected").text());
            $("#BrandName").val($("#BrandID option:selected").text());
            $("#Hardness").val($("#HardnessID option:selected").text());
            $("#MoldNumber").val($("#MoldSelect").val());
            $("#BomVersion").val($("#sltVersion").val());
            if (DetailDrawing.checked)
                $('#DetailDrawing').val('true');
            if (BriefDrawing.checked)
                $('#BriefDrawing').val('true');
            if (PurchaseDrawing.checked)
                $('#PurchaseDrawing').val('true');
            if (ExtraMaching.checked)
                $('#ExtraMaching').val('true');
            $("#PartEdit").submit();
            return true;
        } else {
            alert("请填写黄色必填项");
        }
        //alert($("#PartEdit").serialize());
        //$("#PartEdit").submit();
    })

    //Create a PR by selected parts
    $("#CreatePR").on("click", function () {

        //var id = $("#ProjectGrid").getCell($("#ProjectGrid").getGridParam("selrow"), "ID");
        var selr = $('#PartGrid').jqGrid('getGridParam', 'selarrrow');
        if (selr.length > 0) {
            var ids = "";
            for (var i = 0; i < selr.length; i++) {
                if (ids == "") {
                    ids = $("#PartGrid").getCell(selr[i], "ID");
                } else {
                    ids = ids + "," + $("#PartGrid").getCell(selr[i], "ID");
                }
            }
            location.href = "/Purchase/PRDetail?MoldNumber=" + $("#MoldSelect").val() + "&PartIDs=" + ids;
        } else {
            alert("请选择至少一个零件");
        }
    });


    $("#PartNumber").on("change", function () {
        CheckPNExist($("#PartNumber").val());
    })


    //$("#DeletePart").on("click", function () {
    //    var selr = $('#PartGrid').jqGrid('getGridParam', 'selarrrow');
    //    if (selr.length > 0) {
    //        if (confirm("确认继续删除？")) {
    //            var prjID = $("#sltVersion").val();
    //            $.ajax({
    //                url: "/Project/GetIsPublish?ProjectID=" + prjID,
    //                dataType: "html",
    //                method: "Get",
    //                success: function (msg) {
    //                    if (Number(msg) == 1) {
    //                        alert("项目已发布，不能删除关联的零件.");
    //                    }
    //                    else if (Number(msg) == 0) {
    //                        DeleteParts();
    //                        //} else if (Number(msg) == 2) {
    //                        //    alert("NX端料号不能在网页端删除.");
    //                    }
    //                    else {
    //                        alert("删除失败.");
    //                    }
    //                }
    //            })
    //        }
    //    }
    //    else {
    //        alert("请选择至少一个零件");

    //    }
    //})
    $("#DeletePart").on("click", function () {
        var selr = $('#PartGrid').jqGrid('getGridParam', 'selarrrow');
        if (selr.length > 0) {
            var plID = $("#sltVersion").val();
            $.ajax({
                url: "/Part/GetIsUpgrade?PartListID=" + plID,
                dataType: "html",
                method: "Get",
                success: function (msg) {
                    if (Number(msg) == 1) {
                        alert("PartList已发布，不能删除关联的零件.");
                    }
                    else if (Number(msg) == 2) {
                        DeleteParts();
                    }
                    else {
                        alert("删除失败.");
                    }
                }
            })
            //if (confirm("确认继续删除？")) {                
            //}
        }
        else {
            alert("请选择至少一个零件");
        }
    })

    //added by felix
    //升级partlist版本
    $("#UpgradeModel").on("click", function () {
        var _moldNumber = $("#MoldSelect").val();

        if (_moldNumber != null) {
            if (confirm("确认继续升级？")) {
                $.ajax({
                    url: "/Part/GetIsUpgrade?PartListID=" + $("#sltVersion").val(),
                    dataType: "html",
                    method: "Get",
                    success: function (msg) {
                        if (Number(msg) == 1) {
                            $.ajax({
                                url: "/Part/UpgradePartlist?moldNumber=" + _moldNumber,
                                dataType: "html",
                                method: "Get",
                                success: function (msg) {
                                    if (Number(msg) > 0) {
                                        alert("升级成功");
                                        SetToolBar();
                                        LoadMoldVer(_moldNumber);
                                    } else {
                                        alert("升级失败");
                                    }
                                },
                                error: function (ex) {
                                    alert("升级失败");
                                }
                            })
                        }
                        else if (Number(msg) == 2) {
                            alert("该版本未发布，不能升级.");

                        }
                        else if (Number(msg) == 0) {
                            alert("不是最新版本，不能升级.");
                        }
                        else {
                            alert("升级失败");
                        }
                    },
                    error: function (ex) {
                        alert("升级失败");
                    }
                })


            }
        } else {
            alert("请先选择模具号");
        }
    });
    //发布partlist
    $("#PublishModel").on("click", function () {
        if (confirm("确认继续发布？")) {
            var _moldNumber = $("#MoldSelect").val();

            if (_moldNumber != null) {
                $.ajax({
                    url: "/Part/PublishPartList?MoldNum=" + _moldNumber,
                    dataType: "html",
                    method: "Get",
                    success: function (msg) {
                        if (Number(msg) > 0) {
                            alert("发布成功");
                            //location.reload();
                            SetToolBar();
                            //LoadMoldVer(_moldNumber);
                            ReloadGrid();
                        } else {
                            alert("发布失败");
                        }
                    }
                })


            } else {
                alert("请先选择模具号");
            }
        }
    });

    //升级零件版本
    $("#UpdradePart").on("click", function () {
        var selr = $('#PartGrid').jqGrid('getGridParam', 'selarrrow');
        if (selr.length > 0) {
            if (confirm("确认继续升级？")) {
                var ids = "";
                for (var i = 0; i < selr.length; i++) {
                    if (ids == "") {
                        ids = $("#PartGrid").getCell(selr[i], "ID");
                    } else {
                        ids = ids + "," + $("#PartGrid").getCell(selr[i], "ID");
                    }
                }
                $.ajax({
                    url: "/Part/UpgradePart?ids=" + ids,
                    dataType: "json",
                    method: "Get",
                    success: function (msg) {
                        var jsonObj = eval(msg);
                        console.log(jsonObj);
                        if (jsonObj.length > 0) {
                            var msgStr2 = "以下零件是来自UG，不能升级.";
                            $(jsonObj).each(function (idx, e) {
                                msgStr2 += e.PartName + ";";
                            });
                            if (msgStr2 != "以下零件是来自UG，不能升级.") {
                                alert(msgStr2);
                            }
                            ReloadGrid();
                        }
                        else {
                            alert("零件升级完成");
                            ReloadGrid();
                        }
                    }
                })
            }
        } else {
            alert("请选择至少一个零件");
        }
    });
});


function SetToolBar() {

    var lastestVersion = parseInt($("#sltVersion").find("option").first().attr("value"));
    var NowVersion = parseInt($("#sltVersion").val());
    if (lastestVersion > NowVersion) {
        //$("#CreatePR").attr("disabled", "disabled");
        $("#CreatePart").attr("disabled", "disabled");
        $("#DeletePart").attr("disabled", "disabled");
        $("#PublishModel").attr("disabled", "disabled");
        $("#UpgradeModel").attr("disabled", "disabled");
        $("#UpdradePart").attr("disabled", "disabled");
        //零件不允许编辑
        //$("#PartNumber").attr("disabled", true);
        //$("#PartName").attr("disabled", true);
        //$("#HardnessID").attr("disabled", true);
        //$("#Specification").attr("disabled", true);
        //$("#MaterialID").attr("disabled", true);
        //$("#Quantity").attr("disabled", true);
        //$("#BrandID").attr("disabled", true);
        $("#SavePart").attr("disabled", true);
    }
    else {

        $.ajax({
            url: "/Part/GetPartListJson?PartListID=" + $("#sltVersion").val(),
            dataType: "json",
            method: "Get",
            success: function (msg) {
                var jsonPrj = eval(msg);
                if (jsonPrj.Released == true) {
                    $("#UpgradeModel").removeAttr("disabled");
                    //$("#CreatePR").attr("disabled", "disabled");
                    $("#CreatePart").attr("disabled", "disabled");
                    $("#DeletePart").attr("disabled", "disabled");
                    $("#PublishModel").attr("disabled", "disabled");
                    $("#UpdradePart").attr("disabled", "disabled");
                }
                else {
                    //$("#CreatePR").removeAttr("disabled");
                    $("#CreatePart").removeAttr("disabled");
                    $("#DeletePart").removeAttr("disabled");
                    $("#PublishModel").removeAttr("disabled");
                    $("#UpdradePart").removeAttr("disabled");
                    $("#UpgradeModel").attr("disabled", "disabled");
                }
                //$("#PartNumber").removeAttr("disabled", true);
                //$("#PartName").removeAttr("disabled", true);
                //$("#HardnessID").removeAttr("disabled", true);
                //$("#Specification").removeAttr("disabled", true);
                //$("#MaterialID").removeAttr("disabled", true);
                //$("#Quantity").removeAttr("disabled", true);
                //$("#BrandID").removeAttr("disabled", true);
                $("#SavePart").removeAttr("disabled", true);
            }
        })
    }
}

//Display the select mold number dialog
function ShowMoldSelect() {
    $("#ProjectSelect").modal("show");
    $("#target").val("/Part/Index?MoldNumber=");
}

//When users input keywords, load the mold numbers contains the keyword
//function LoadMolds(Keyword) {
//    $("#MoldList option").remove();
//    $.getJSON("/Project/JsonMoldNumber?Keyword=" + Keyword, function (msg) {
//        $.each(msg, function (i, n) {
//            $("#MoldList").append($("<option/>", {
//                value: n.ProjectID,
//                text: n.MoldNumber
//            }))
//        });
//    })
//}

////When project is selected, jump to the project part list;
//function SelectProject() {
//    location.href = "/Part/Index?ProjectID="+$("#MoldList option:selected").val();
//}

//Load the brands to dropdown list
function LoadBrands() {
    $("#BrandID option").remove();
    $("#BrandID").append($("<option/>", {
        value: 0,
        text: "-"
    }))
    $.getJSON("/Administrator/Service_GetBrandsByType?_type=模具材料", function (msg) {
        $.each(msg, function (i, n) {
            $("#BrandID").append($("<option/>", {
                value: n.BrandID,
                text: n.Name
            }))
        });
    })
}

function LoadSuppliers() {
    $("#SupplierID option").remove();
    $("#SupplierID").append($("<option/>", { value: 0, text: '-' }));
    $.getJSON("/Part/JsonSuppliers", function (msg) {
        $.each(msg, function (i, n) {
            $("#SupplierID").append($("<option/>", {
                value: n.SupplierID,
                text: n.Name
            }));
        })
    })
}

//Load the project id and mold number information to dialog
function LoadProject(MoldNumber) {
    $.getJSON("/Project/JsonProject?MoldNumber=" + MoldNumber, function (msg) {
        $("#MoldNumber").val(msg.MoldNumber);
        $("#PartNumber").val(msg.MoldNumber + "-");
        $("#ProjectID").val(msg.ProjectID);
    });
}

function Cb(jub,box) {
    if (jub) {
        box.checked = true;
        //box.setAttribute('checked', true);
    }
    else {
        box.checked = false;
    }
}

function LoadPart(PartID) {
    var ajax = "/Part/JsonPart?PartID=" + PartID;
    $.getJSON(ajax, function (msg) {
        var DetailDrawing = document.getElementById('DetailDrawing');
        var BriefDrawing = document.getElementById('BriefDrawing');
        var PurchaseDrawing = document.getElementById('PurchaseDrawing');
        var ExtraMaching = document.getElementById('ExtraMaching');
        $("#PartID").val(msg.PartID);
        $("#ProjectID").val(msg.ProjectID);
        $("#Enabled").val(msg.Enabled);
        $("#PartNumber").val(msg.PartNumber);
        $("#PartName").val(msg.Name);
        $("#Specification").val(msg.Specification);
        $("#Quantity").val(msg.Quantity);
        $("#MaterialName").val(msg.MaterialName);
        $("#JobNo").val(msg.JobNo);
        $("#ShortName").val(msg.ShortName);
        Cb(msg.DetailDrawing, DetailDrawing);
        Cb(msg.BriefDrawing, BriefDrawing);
        Cb(msg.PurchaseDrawing, PurchaseDrawing);
        Cb(msg.ExtraMaching, ExtraMaching);
        $("#Memo").val(msg.Memo);
        LoadMaterials(msg.MaterialID);
        $("#MaterialID option[value=" + msg.MaterialID + "]").attr("selected", "true");
        if (msg.Hardness != "") {
            LoadHardness(msg.MaterialID, msg.Hardness);
        }
        $("#BrandID").val(msg.BrandID);
        $("#BrandName").val(msg.BrandName);
        $("#Version").val(msg.Version);
        if (msg.FromUG) {
            $("#FromUG").html("该零件信息只能在UG中进行修改");
            $("#SavePart").attr("disabled", true);
        }
        //by michael
        if (msg.Status > 0 && msg.Locked == false && msg.FromUG == false) {
            $("#Specification").removeAttr("disabled", true);
            $("#MaterialID").removeAttr("disabled", true);
            $("#Quantity").removeAttr("disabled", true);
            $("#BrandID").removeAttr("disabled", true);
        }
    });
    $("#PartEditModal").modal("show");
}

//Validate the required fields when creating part
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
    return RequiredFieldValid;
}

function CheckPNExist(PartNumber) {
    $.ajax({
        url: "/Part/CheckPartNumberExist?PartNumber=" + PartNumber,
        dataType: "html",
        method: "Get",
        success: function (msg) {
            if (Number(msg) > 0) {
                alert("同样零件号已经存在");
                $("#PartNumber").addClass("invalidefield");
            }
        }
    })
}


function DeleteParts() {
    var selr = $('#PartGrid').jqGrid('getGridParam', 'selarrrow');
    if (selr.length > 0) {

        var ids = "";
        var pns = "";
        for (var i = 0; i < selr.length; i++) {
            if (ids == "") {
                ids = $("#PartGrid").getCell(selr[i], "ID");
                pns = $("#PartGrid").getCell(selr[i], "MaterialNo");
            } else {
                ids = ids + "," + $("#PartGrid").getCell(selr[i], "ID");
                pns = pns + "\r" + $("#PartGrid").getCell(selr[i], "MaterialNo");
            }
        }

        if (confirm("确认删除以下零件？\r" + pns)) {
            $.ajax({
                url: "/Part/DeleteParts?PartIDs=" + ids,
                dataType: "json",
                method: "Get",
                success: function (msg) {
                    if (msg.Code == 1) {
                        alert(msg.Message);
                        ReloadGrid();
                    }
                    else {
                        alert(msg.Message);
                    }                    
                    //var jsonObj = eval(msg);
                    //if (jsonObj.length > 0) {
                    //    var msgStr = "以下零件已采购不能删除.";
                    //    var msgStr2 = "以下零件是来自UG，不能删除.";
                    //    $(jsonObj).each(function (idx, e) {
                    //        if (e.Type == 1) {
                    //            msgStr += e.PartName + ";";
                    //        }
                    //        else {
                    //            msgStr2 += e.PartName + ";";
                    //        }
                    //    });
                    //    if (msgStr == "以下零件已采购不能删除.") {
                    //        alert(msgStr2);
                    //    }
                    //    else if (msgStr2 == "以下零件是来自UG，不能删除.") {
                    //        alert(msgStr);
                    //    }
                    //    else {
                    //        alert(msgStr + "\r\n" + msgStr2);
                    //    }
                    //    ReloadGrid();
                    //}
                    //else {
                    //    alert("零件删除完成");
                    //    ReloadGrid();
                    //}

                }
            })
        }


    } else {
        alert("请选择至少一个零件")
    }
}


function LoadMoldList(Keyword, MoldNumber) {
    var _url = "/Part/GetMoldNumberList";
    if (Keyword != undefined) {
        _url = _url + "?Keyword=" + Keyword;
    }


    $("#MoldSelect option").remove();


    $.getJSON(_url, function (msg) {
        $.each(msg, function (i, n) {
            var _val = $.trim(n);
            if (_val != "") {
                if (MoldNumber == _val) {
                    $("#MoldSelect").append($("<option/>", {
                        text: _val,
                        value: _val,
                        selected: true
                    }))
                } else {
                    $("#MoldSelect").append($("<option/>", {
                        text: _val,
                        value: _val
                    }))
                }

            }
        })
       
        
        if (MoldNumber == undefined || MoldNumber == "")
        {
            //MoldNumber = $("#MoldSelect").find("option").first().text();
            //MoldNumber = '';
        }
        console.log(MoldNumber);
        LoadMoldVer(MoldNumber);
    })
}


function LoadMoldVer(MoldNumber) {
    var _url_ver = "/Part/GetMoldVerList?MoldNumber=" + MoldNumber;
    console.log(_url_ver);
    $("#sltVersion option").remove();
    $("#sltVersion").empty();
    $.getJSON(_url_ver, function (msg) {
        var jsonObj = eval(msg);
        $.each(jsonObj, function (i, n) {
           
            $("#sltVersion").append($("<option/>", {
                text: (n.Version+'').length == 1 ? "0" + n.Version : n.Version,
                value: n.PartListID
            }));
        });
        SetToolBar();
        var _PartListID = $("#sltVersion").val();
        //加载零件清单
        var _url = "/Part/GetJsonPartsByBomID?PartListID=" + _PartListID + "&MoldNumber=" + MoldNumber;

        $("#PartGrid").jqGrid('setGridParam', { datatype: 'json', url: _url }).trigger("reloadGrid");
    })
}

function ReloadGrid() {
    var _PartListID = $("#sltVersion").val();
    var _MoldNumber = $("#MoldSelect").val();
    if (_MoldNumber != null && _PartListID!=null) {
        SetToolBar();
        //加载零件清单
        var _url = "/Part/GetJsonPartsByBomID?PartListID=" + _PartListID + "&MoldNumber=" + _MoldNumber;

        $("#PartGrid").jqGrid('setGridParam', { datatype: 'json', url: _url }).trigger("reloadGrid");
    }
}