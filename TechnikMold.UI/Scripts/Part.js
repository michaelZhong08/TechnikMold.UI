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
    $("#MoldList").on("dblclick", function () {
        SelectProject();
    });
    //Select the project and jump to the project part list
    //If no project is selected, do nothing
    $("#SelectProject").on("click", function () {
        if ($("#MoldList option:selected").val() != undefined)
        {
            SelectProject();
        }
        //
    });

    $("#CreatePart").on("click", function () {
        var _moldNumber = $("#MoldSelect").val();
 
        if (_moldNumber != null) {
            $("#MoldNumber").val(_moldNumber);
            $("#PartNumber").val(_moldNumber + "-");
            $("#PartName").val(_moldNumber + "_");
            $("#PartEditModal").modal("show");
        } else {
            alert("请先选择模具号");
        }
        
    });

    //Process the fields when part edit dialog shows
    $("#PartEditModal").on("shown.bs.modal", function () {
        if ($("#PartID").val()==0){
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

    $("#SavePart").on("click", function () {
        
        //alert($("#PartEdit").serialize());
        if (ValidateCreate()) {
            $("#MaterialName").val($("#MaterialID option:selected").text());
            //$("#BrandName").val($("#BrandID option:selected").text());
            $("#BrandName").val($("#BrandID option:selected").text());
            $("#Hardness").val($("#HardnessID option:selected").text());
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
            var ids="";
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


    $("#DeletePart").on("click", function () {
        DeleteParts();
    })

});


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
    $.getJSON("/Part/JsonBrands", function (msg) {
        $.each(msg, function (i, n) {
            $("#BrandID").append($("<option/>", {
                value: n.BrandID,
                text:n.Name
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



function LoadPart(PartID) {
    var ajax = "/Part/JsonPart?PartID=" + PartID;
    $.getJSON(ajax, function (msg) {
        $("#PartID").val(msg.PartID);
        $("#ProjectID").val(msg.ProjectID);
        $("#Enabled").val(msg.Enabled);
        $("#PartNumber").val(msg.PartNumber);
        $("#PartName").val(msg.Name);
        $("#Specification").val(msg.Specification);
        $("#Quantity").val(msg.Quantity);
        $("#MaterialName").val(msg.MaterialName);
        $("#MoldNumber").val($("#MoldSelect").val());

        
        $("#Memo").val(msg.Memo);
        LoadMaterials(msg.MaterialID);
        $("#MaterialID option[value=" + msg.MaterialID + "]").attr("selected", "true");
        if (msg.Hardness != "") {
            LoadHardness(msg.MaterialID, msg.Hardness);
        }
        
        //$("#HardnessID option[text=" + msg.Hardness + "]").attr("selected", "true");
        //$("#Hardness").val(msg.Hardness);
        //$("#BrandID option[value=" + msg.BrandID + "]").attr("selected", "true");
        //$("#BrandName").val(msg.BrandName);
        //$("BrandID option[value=" + msg.BrandID + "]").attr("selected", "true");
        $("#BrandID").val(msg.BrandID);
        $("#BrandName").val(msg.BrandName);
        $("#Version").val(msg.Version);
        if (msg.FromUG) {
            $("#FromUG").html("该零件信息只能在UG中进行修改");
            $("#SavePart").attr("disabled", true);
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
                pns = pns+"\r"+$("#PartGrid").getCell(selr[i], "MaterialNo");
            }
        }

        if (confirm("确认删除以下零件？\r" + pns)) {
            $.ajax({
                url: "/Part/DeleteParts?PartIDs=" + ids,
                dataType: "html",
                method: "Get",
                success: function (msg) {
                    if (Number(msg) > 0) {
                        alert("零件删除完成");
                        location.reload();
                    } else {
                        alert("操作失败");
                    }
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
        _url=_url+"?Keyword="+Keyword;
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

