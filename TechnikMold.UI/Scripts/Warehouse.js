/// <reference path="Warehouse.js" />
$("document").ready(function () {

    var _listsize = Math.round((document.documentElement.clientHeight) / 24.5);
    $("#MoldSelect").attr("size", _listsize);


    $("#POState").on("change", function () {
        AlterState();
    });


    $("#ConfirmInStock").on("click", function () {
        ConfirmInStock();
    })

    $("#ClosePO").on("click", function () {
        ClosePO();
    })

    $("#StockQuery").on("click", function () {
        $("#StockQueryDlg").modal("show");
    })

    $("#StockQueryBtn").on("click", function () {
        location.href = "/Warehouse/Index?keyword=" + $("#keyword").val();
    })

    $("#ShowHistory").on("click", function () {
        ShowHistory();
    })
    $("#NewPR").on("click", function () {
        location.href = "/Purchase/PRDetail";
    })

    $("#StockInOutBtn").on("click", function () {
        StockChange();
    })


})

function AlterState() {
    location.href = "/Warehouse/WHPOList?State=" + $("#POState option:selected").val();
}


function SetList(state) {
    $("#POState").val(state);
}

function ShowPOContentToStockDlg(PurchaseItemID) {
    var _url = "/Warehouse/JsonPOContent?PurchaseItemID=" + PurchaseItemID;
    $.getJSON(_url, function (msg) {
        $("#PurchaseItemID").val(msg.PurchaseItemID);
        $("#Name").val(msg.Name);
        $("#Specification").val(msg.Specification);
        $("#Quantity").val(msg.Quantity);
        $("#ReceiveQty").val(0);
        $("#PartNumber").val(msg.PartNumber)
        $("#ReceivedQty").val(msg.InStockQty)
        $("#POContentToStockDlg").modal("show");
    })

    _url = "/Warehouse/JsonStockByPO?PurchaseItemID=" + PurchaseItemID;
    $.getJSON(_url, function (msg) {
        if (msg != ""){
            if (msg.WarehouseID > 0) {
                $("#WarehouseList").val(msg.WarehouseID);
                $("#WarehouseList").attr("readonly", true)
            }
            if ((msg.WarehouseID > 0) && (msg.WarehousePositionID > 0)) {
                LoadWarehousePosition(msg.WarehouseID, msg.WarehousePositionID);
                $("#WarehousePositionList").attr("readonly", true)
            }
            
        }
    })
}

function ConfirmInStock() {
    var _left = Number($("#Quantity").val()) - Number($("#ReceivedQty").val());
    var _qty = Number($("#ReceiveQty").val());
    var _accept;
    console.log("Receive=" + _qty + ",Left=" + _left);
    if (_qty > _left) {
        if (confirm("收货数量大于采购数量，是否确认收货？")){
            _accept = true;
        } else {
            _accept = false;
        }
    } else {
        _accept = true;
    }
    if (_accept) {
        if (Number($("#ReceiveQty").val()) <= 0) {
            alert("收货数量必须大于0");
        } else {
            //var q1 = $("#ReceiveQty").val();
            //var q2 = $("#ReceivedQty").val();
            //var q3 = $("#Quantity").val();
            if (Number($("#ReceiveQty").val()) + Number($("#ReceivedQty").val()) > Number($("#Quantity").val())) {
                alert("无法超量收货")
            } else {
                var _url = "/Warehouse/POContentInStock";
                $("#WarehouseID").val($("#WarehouseList").val());
                $("#WarehousePositionID").val($("#WarehousePositionList").val());
                //alert($("#POContentToStock").serialize());
                $.ajax({
                    url: _url,
                    datatype: "html",
                    type: "Post",
                    data: $("#POContentToStock").serialize(),
                    error: function () {

                    },
                    success: function (msg) {

                        $("#POContentToStockDlg").modal("hide");
                        if (Number(msg) == 0) {
                            alert("零件入库信息已记录,订单收货完成");
                        } else if (Number(msg) > 0) {
                            alert("零件入库信息已记录");
                        } else {
                            alert("无法超量收货")
                        }
                        location.reload()
                    }
                });
            }
            
        }
    }
    
}

function ClosePO() {
    var PurchaseOrderID = $("#PurchaseOrderID").val();
    location.href = "/Warehouse/ClosePurchaseOrder?PurchaseOrderID=" + PurchaseOrderID;
    //if (confirm("确认要返回订单？")) {
    //    location.href = "/Warehouse/ClosePurchaseOrder?PurchaseOrderID=" + PurchaseOrderID;
    //}
}

//---------------Context Menu------------------
function ShowModifyStockDialog(type) {
    //if (type == 1) {
    //    $("#StockInOutLable").html("出库");
    //} else if(type==2) {
    //    $("#StockInOutLable").html("入库");
    //}
    //$("#StockInOutDlg").modal("Show");
    if (Number(type) == 1) {
        $("#StockChangeLable").html("零件出库");
        $("#Type").val(-1);
    } else {
        $("#StockChangeLable").html("零件入库");
        $("#Type").val(1);
    }

    $("#StockChangeDlg").modal("show");
}

function ShowHistory() {
    $("#POHistory option").remove();
    var _id = $("#PurchaseOrderID").val();
    var ajax = "/Warehouse/JsonHistory?PurchaseOrderID=" + _id
    $.getJSON(ajax, function (msg) {
        $.each(msg, function (i, n) {
            var _date = renderDate (n.Date);

            //$("#PRHistoryList").append("<option/>", {
            //    value: n.ProcessRecordID,
            //    text: n.Message

            //});
            var _message=n.Name + ", 数量:" + n.Quantity + "。备注信息:" + n.Memo;
            switch (n.RecordType) {
                case 1:
                    _message = "接收零件" + _message;
                    break;
                case 2:
                    _message = "出库零件" + _message;
                    break;
                case 3:
                    _message = "退货" + _message;
                    break;
            }
            $("#POHistory").append($("<option/>", {
                value: n.WarehouseRecordID,
                text: _date + "  " +_message
            }))
        })
    })
    $("#POHistoryDialog").modal("show");
}

function StockChange() {
    var _stockID = GetCellContent("StockItemGrid", "WarehouseStockID");
    var _qty = Number($("#Quantity").val()) * Number($("#Type").val());
    var _url = "/Warehouse/StockChange?WarehouseStockID=" + _stockID + "&Quantity=" + _qty;
    $.ajax({
        type: "GET",
        dataType: "html",
        url: _url,
        error: function () {

        },
        success: function (msg) {
            alert(msg);
            location.reload();
        }
    });
}


function ValidateCreate(FormName) {
    var RequiredFieldValid = true;
    var PhaseDateValid = true;
    var errorMessage = "";
    //Required field is filled
    $("#"+FormName+" input.required").each(function () {
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