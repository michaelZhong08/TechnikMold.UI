$(document).ready(function () {
    var _listsize = Math.round((document.documentElement.clientHeight - 210) / 18);
    $("#MoldSelect").attr("size", _listsize);

    $("#NewPR").on("click", function () {
        location.href = "/Purchase/PRDetail";
    })



    //Submit the whole PR content list to system
    $("#CreatePR").on("click", function () {
        if ($("#DueDate").val() != "" ) {
            //$("#CreatePR").attr("disabled", "true");
            CreatePR();
        } else {
            $("#DueDate").addClass("")
            $("#DueDate").addClass("invalidefield");
            alert("请输入需求日期！");
        }

    });
    $("#GetERPID").on("click", function () {
        GetERPID();

    });
    $("#PRState").on("change", function () {
        ChangeGridStatus();
        //location.href = "/Purchase/Index?State=" + $("#PRState option:selected").val();
    })

    //Display the  add Purchase request content dialog
    $("#AddContent").on("click", function () {
        //$("#MaterialID option:first-child").attr("selected", "true");
        //LoadHardness($("#MaterialID option:selected").val());
        //ClearPRContent();
        //$("#row").val(0);
        //if ($("#PurchaseType").val() == 2) {
        //    $("#MoldNumber").val("XX")
        //}
        //$("#PRContentAdd").modal("show");
        console.log($("#row").val());
        $('#tb_PartSearch').jqGrid('clearGridData');
        $("#SpecKeyword").val('');
        var _purtype = Number($('#PurchaseType').val());
        if (_purtype > 0) {            
            $("#PartSearch").modal("show");
        }
        else {
            alert('请先选择采购类型！');
            return;
        }
    });

    //Load the material hardness when changing the material
    $("#MaterialID").on("change", function () {
        LoadHardness(this.value);
    })

    //Create a purchase item and add it to the grid without any database operation
    $("#SaveContent").on("click", function () {
        if (ValidateCreate("PRContentAdd")) {
            if (ValidateZero("Quantity")) {
                console.log($("#row").val());
                AddPRContent(Number($("#row").val()));
                $("#PRContentAdd").modal("hide");
            } else {

                alert("零件数量不能为0");
            }
        } else {
            alert("请填写黄色必填项");
        }
        $("#PRModified").val("true");
    })




    ////Display  the project select modal and load the mold numbers
    $("#MoldNumber").on("click", function () {
        $("#ProjectSelect").modal("show");
        LoadMolds("");
    });

    //Display the create supplier modal
    $("#AddSupplier").on("click", function () {
        ClearSupplierDialog();
        $("#SupplierEdit").modal("show");
    })

    //Submit the changes to supplier
    $("#SaveSupplier").on("click", function () {
        //alert($("#SupplierInfo").serialize());
        if (ValidateCreate("SupplierInfo")) {
            SaveSupplierInfo();
        } else {
            alert("请填写黄色必填项");
        }

    })


    //Display the supplier select dialog
    $("#SelectSupplier").on("click", function () {
        LoadPRSuppliers($("#PurchaseRequestID").val(), "SupplierList");
        $("#SelectSupplierDialog").modal("show");
        $("#SupplierList option").remove();
    })

    //Add the selected supplier to list
    $("#Select").on("click", function () {
        PickSupplier();
    });

    $("#Remove").on("click", function () {
        RemoveSupplier();
    })

    $("#AvailableSuppliers").on("dblclick", function () {
        PickSupplier();
    })

    $("#SupplierList").on("dblclick", function () {
        RemoveSupplier();
    })

    //Save the supplier selection
    $("#SelectSuppliers").on("click", function () {

    })

    $("#DeleteSupplier").on("click", function () {

        var _ids = GetMultiSelectedCell("SupplierGrid", "SupplierID");
        var _id = _ids.split(',')[0];
        ////console.log(_ids + ",Length=" + _ids.length + ",first=" + _id);
        if (_ids.length == 0) {
            alert("请先选择供应商");
        } else {
            if (confirm("确认要删除供应商？")) {
                DeleteSupplier(_id);
            }
        }
    })

    //Display the project select dialog and assign the select action
    $("#SearchPR").on("click", function () {
        $("#PRSearchDialog").modal("show");
    })
    $("#ExportExcelForPurchase").on("click", function () {
        ExportExcelForPurchase();
    })

    //Save the quotation information
    $("#SaveQuotation").on("click", function () {
        if ($("#QRSupplierList").val() != "") {
            var _tax=$('#TaxRate').val();
            if (_tax != '' && _tax != undefined) {
                $("#SupplierID").val($("#QRSupplierList option:selected").val());
                //if ((ValidateCreate("QRQuotationInput")) && (ValidateQuotation())) {
                if (ValidateQuotation()) {
                    SubmitQuotation();
                    //$("#QRQuotationInput").submit();
                } else {
                    return false;
                }
            }
            else {
                alert("请选择税率");
                return false;
            }
        } else {
            alert("请选择供应商");
            return false;
        }

    })

    //Display the quotation maintenance page
    $("#UpdateQuotation").on("click", function () {
        location.href = "/Purchase/PRQuotation?PurchaseRequestID=" + $("#PurchaseRequestID").val();
    })

    //Display the supplier confirmation dialog
    $("#AssignSupplier").on("click", function () {
        if (Number($("#TotalPrice").val()) > 0 || Number($("#TotalPriceWT").val())>0) {
            $("#SupplierName").val($("#AssignedSupplier option:selected").text());
            console.log($("#TotalPrice").val());
            $("#Total").val($("#TotalPrice").val());
            console.log($("#TotalPriceWT").val());
            $("#TotalWT").val($("#TotalPriceWT").val());
            $("#AssignSupplierDialog").modal("show");
            $("#SupplierID").val($("#AssignedSupplier option:selected").val());
        } else {
            alert("该供应商尚未报价");
        }
    })

    //Confirm the supplier selection and save to system
    $("#ConfirmSupplier").on("click", function () {
        if (ValidateCreate("Assign")) {
            $("#Assign").submit();
        }

    })

    //Display the quotation summary page
    $("#CompareQuotation").on("click", function () {
        //判断是否已输入报价
        $.get('/Purchase/Service_QR_ChkQuotations?quotationRequestID=' + $('#QuotationRequestID').val(), function (res) {
            if (res>0) {
                location.href = "/Purchase/QuotationSummary?QuotationRequestID=" + $("#QuotationRequestID").val();
            } else {
                alert('请先输入供应商报价！');
                return false;
            }
        })
        //
        
    })


    $("#AssignedSupplier").on("change", function () {
        UpdateTotal();
        //DisplayTotal();
    })

    $("#Approve").on("click", function () {
        $("#PRReview").submit();
    })

    $("#Reject").on("click", function () {
        $("#ResponseType").val("false");
        $("#PRReview").submit();
    })

    $("#PRRelease").on("click", function () {
        location.href = "/Purchase/PurchaseRequestAccepted?PurchaseRequestID=" + $("#PurchaseRequestID").val();
    })

    $("#PRFinish").on("click", function () {
        location.href = "/Purchase/PurchaseRequestInStock?PurchaseRequestID=" + $("#PurchaseRequestID").val();
    });

    //$("#SupplierList").on("click", function () {
    //    var SupplierID = $("#SupplierList option:selected").val();
    //    LoadSupplierInfo(SupplierID);
    //})



    $("#SupplierKeyword").on("keyup", function () {
        LoadSupplierList($("#SupplierKeyword").val());
    })

    $("#EditSupplier").on("click", function () {
        var _ids = GetMultiSelectedCell("SupplierGrid", "SupplierID");
        var _id = _ids.split(',')[0];
        //console.log(_ids + ",Length=" + _ids.length + ",first=" + _id);
        if (_ids.length == 0) {
            alert("请先选择供应商");
        } else {

            LoadSupplier(_id);
        }

    })

    $("#PRContentAdd").on("shown.bs.modal", function () {
        //LoadBrands();
    })

    //$("#AddContact").on("click", function () {
    //    if (($("#SupplierList").val() == undefined) && ($("#SelectedSupplierID").val() == "")) {
    //        alert("请先选择供应商");
    //    } else {
    //        $("#OrganizationID").val($("#SelectedSupplierID").val());
    //        $("#ContactFullName").val("");
    //        $("#ContactID").val(0);
    //        $("#Enabled").val(true);
    //        $("#Email").val("");
    //        $("#Telephone").val("");
    //        $("#Mobile").val("");
    //        $("#Memo").val("");
    //        $("#SupplierContact").modal("show");
    //        $("#DeleteContact").attr("style", "display:none");
    //    }        
    //})

    $("#SaveContact").on("click", function () {
        SaveContact();
    })

    $("#DeleteContact").on("click", function () {
        if (confirm("确定删除供应商联系人?")) {
            location.href = "/Purchase/DeleteContact?ContactID=" + $("#ContactID").val();
        }
    })


    $("#PRHistory").on("click", function () {
        ShowPRHistory($("#PurchaseRequestID").val());
    });

    $("#DeleteContent").on("click", function () {
        DeletePRContent();
    })

    $("#RestartPR").on("click", function () {
        $("#RestartPRDialog").modal("show");
    })

    $("#RestartPRBtn").on("click", function () {
        RestartPR();
    })


    $("#SubmitPR").on("click", function () {
        //var _selr = $("#PRContentGrid").jqGrid("getDataIDs");
        //var _check = true;
        //for (var i = 1; i <= _selr.length; i++) {
        //    var _reqDate = $("#PRContentGrid").getCell(i, "RequireTime");
        //    if (_reqDate == "-") {
        //        _check = false;
        //    }
        //}
        //if (!_check) {
        //    alert("请输入各个采购件需求日期")
        //} else {
        //    if (confirm("确认提交采购申请？")) {
        //        SubmitPR($("#PurchaseRequestID").val());
        //    }
        //}

        ////未获取erp料号不允许提交申请单  by michael
        //var rowData = $("#PRContentGrid").jqGrid("getRowData");
        //var m = true;
        //var alertMsg = "";
        //for (var i = 0; i < rowData.length; i++) {
        //    if (rowData[i].ERPPartID == "" || rowData[i].ERPPartID==0) {
        //        alertMsg = rowData[i].JobNo + ";" + alertMsg;
        //        m = false;
        //    }
        //}
        //if (!m) {
        //    alert("零件号[" + alertMsg + "]的ERP料号为空，请点击'同步ERP料号'进行同步！");
        //    return false;
        //};
        CreatePR(true);
        //未获取erp料号不允许提交申请单  by michael       
    })

    $("#PRSubmitButton").on("click", function () {
        var PurchaseRequestID = $("#PurchaseRequestID").val();
        var Memo = $("#SubmitMemo").val();
        SubmitPR(PurchaseRequestID, Memo);
    })

    $("#ReviewPR").on("click", function () {
        $("#PRReviewModal").modal("show");
    })

    $("#PRReviewButton").on("click", function () {
        var PurchaseRequestID = $("#PurchaseRequestID").val();
        var Memo = $("#ReviewMemo").val();
        ReviewPR(PurchaseRequestID, Memo);
    })

    $("#CancelPR").on("click", function () {
        if (confirm("确认取消采购申请？")) {
            var PurchaseRequestID = $("#PurchaseRequestID").val();
            CancelPR(PurchaseRequestID);
        }
    })


    $("#BatchApprove").on("click", function () {
        var _ids = GetMultiSelectedCell("PRListGrid", "ID");
        if (_ids == "") {
            alert("请至少选择一个申请单");
        } else {
            if (confirm("确认批准选中的申请单？")) {
                BatchApprove(_ids);
            }
        }
    })

    //$("#RecommandSupplier").on("click", function () {
    //    $("#RecommandSupplierModal").modal("show");LoadRecommandSuppliers
    //$("#RecommandSupplierModal").on("shown.bs.modal", function () {
    //    $("#Suppliers option").remove();
    //    LoadRecommandSuppliers();
    //})

    //$("#RecommandSupplierName").on("focus", function () {
    //    $("#RecommandSupplierModal").modal("show");
    //})

    //$("#RecommandSupplierBtn").on("click", function () {
    //    $("#SupplierID").val($("#Suppliers option:selected").val());
    //    $("#RecommandSupplierName").val($("#Suppliers option:selected").text());
    //    $("#RecommandSupplierModal").modal("hide");
    //})

    //----------------------------------------------------------------------------

    //---------------------Quotation Request responses start----------------------
    $("#NewQR").on("click", function () {
        location.href = "/Purchase/QRDetail";
    })

    $("#CreateQR").on("click", function () {
        CreateQR();
    })

    $("#SaveQR").on("click", function () {
        if (ValidateQR()) {
            SaveQR();
        }

    })

    $("#CancelQR").on("click", function () {
        CancelQR();
    })


    $("#CloseQR").on("click", function () {
        CloseQR();
    })

    $("#DeleteQRContent").on("click", function () {
        DeleteQRContent();
    })

    $("#AddQRContent").on("click", function () {
        ClearQRContent();
        $("#QRContentAdd").modal("show");
    })

    $("#SaveQRContent").on("click", function () {
        if (ValidateCreate("")) {
            if (ValidateZero("Quantity")) {
                AddQRContent();
                $("#QRContentAdd").modal("hide");
            } else {

                alert("零件数量不能为0");
            }
        } else {
            alert("请填写黄色必填项");
        }
    })

    $("#SelectQRSupplier").on("click", function () {
        SelectQRSupplier();
    })

    $("#SaveQRSuppliers").on("click", function () {
        //SaveQRSuppliers();
    })


    $("#SendQRMail").on("click", function () {
        if ($("#QRReceiver").val() != "") {
            MailResult();
        } else {
            alert("请选择或输入供应商询价单接受人");
        }

        //$("#SendPRByMail").submit();
    });

    $("#SendQR").on("click", function () {
        SendQR();
    })

    $("#QRContactList").on("dblclick", function () {
        LoadEmail($("#QRContactList option:selected").val());
    })

    $("#QuotationInput").on("click", function () {
        $.get("/Purchase/Service_QR_GetQRSuppliers?quotationID=" + $('#QuotationRequestID').val(), function (msg) {
            if (msg.length > 0) {
                location.href = "/Purchase/QuotationInput?QuotationRequestID=" + $("#QuotationRequestID").val();
            } else {
                alert('请选择包含有供应商的报价组！');
            }
        });       
    })
    $("#RestartQR").on("click", function () {
        if (confirm("确定要重新询价？")) {
            RestartQR();
        }
    })

    //----------------------------------------------------------------------------


    //---------------------Purchase Order responses start----------------------

    $("#DeletePOContent").on("click", function () {
        DeletePOContent();
    })

    $("#SubmitPO").on("click", function () {
        SubmitPO();
    })

    $("#ReviewPO").on("click", function () {
        $("#POReviewModal").modal("show");
    })

    $("#POReviewBtn").on("click", function () {
        ReviewPO();
    })

    //--unfinish-------
    $("#SendPO").on("click", function () {
        SendPO();
    })

    $("#POState").on("change", function () {
        //location.href = "/Purchase/PurchaseOrderList?State=" + $("#POState").val();
        RefreshPOByState();
    })

    $("#QuotationAgain").on("click", function () {
        location.href = "/Purchase/QRDetail?QuotationRequestID=" + $("#QuotationRequestID").val();
    })

    $("#QRState").on("change", function () {
        ChangeGridStatus()
        //location.href="/Purchase/QuotationRequestList?State="+$("#QRState").val();
    })

    $("#SaveQRContentQty").on("click", function () {
        SaveQRContentQty();
    })

    $("#ExportStdData").on("click", function () {
        ExportStdData();
    })

    $("#SupplierContacts").on("click", function () {
        //alert("AAA");
        SupplierContacts();
    })

    $("#ContactList").on("click", function () {
        LoadContactInfo();
    })

    $("#AddContact").on("click", function () {
        AddContact();
    })

    $("#SupplierContact").on("shown.bs.modal", function () {
        ResetContactDialog();
    })


    $("#DelContact").on("click", function () {
        DelContact();
    })
    //----------------------------------------------------------------------------


    $("#SearchPO").on("click", function () {
        $("#POSearchDialog").modal("show");
    })

    $("#POSearchDialog,#PRSearchDialog").on("shown.bs.modal", function () {
        LoadMoldNumbers();

        LoadSuppliers();
        $("#AvailableSuppliers").append($("<option/>", {
            value: 0,
            text: "-",
            selected: true
        }))
    })

    $("#MoldKeyword").on("keyup", function () {
        LoadMoldNumbers($("#MoldKeyword").val())
    })
});

///同步且获取没有erp料号的零件
function GetERPID() {
    var rowData = $("#PRContentGrid").jqGrid("getRowData");
    var itemData = "";
    var name = "PRContents";

    var PurchaseRequestID = $('#PurchaseRequestID').val();
    $.ajaxSettings.async = 'false';
    $.get('/Purchase/Service_PR_Vaild_ExprotExcelForPart?PurchaseRequestID=' + PurchaseRequestID, function (msg) {
        if (msg == 'ok') {
            location.href = "/Purchase/ExportExcelForPartByPR?PurchaseRequestID=" + PurchaseRequestID;
        }
        //更新页面字段 ERP料号
        var rows = $('#PRContentGrid').getDataIDs();
        for (var i = 0; i < rows.length; i++) {
            $.ajax({
                type: "Post",
                async: false,
                url: "/Purchase/GetErpIDByPrcID",
                data: { 'PrcID': rowData[i].ID },
                success: function (result) {
                    if (result != '') {
                        $("#PRContentGrid").setCell(rows[i], 'ERPPartID', result);
                    }
                }
            })
        };
        //更新按钮状态
        $.ajax({
            url: "/Purchase/IsErpPasrts",
            type: "Get",
            async: false,
            data: { 'prID': PurchaseRequestID },
            success: function (result) {
                if (result != 'ok') {
                    return
                }
                else {
                    $('#GetERPID').css('pointer-events', 'none');
                    $('#GetERPID').css('cursor', 'not-allowed');
                    $('#GetERPID').css('background-color', 'darkgray');
                    $('#GetERPID').css('border-color', 'darkgray');

                }
            }
        });
    }, 'html');
    
    //$.ajax({
    //    type: "Post",
    //    dataType: "html",
    //    //async: false,
    //    url: "/Purchase/ExportExcelForPartByPR?PurchaseRequestID=" + PurchaseRequestID,
    //    data: {},
    //    error: function () {
    //        PRContentGrid("", $("#PurchaseRequestID").val(), "", "");
    //    },
    //    success: function (msg) {
            
    //    }
    //});
}
//导出采购申请
function ExportExcelForPurchase() {
    var _ids = GetMultiSelectedCell("PRListGrid", "ID");
    if (_ids != "") {
        _ids = _ids + ",";
        ////2019/1/17 添加
        $.ajaxSettings.async = false;
        $.get('/Purchase/Service_PR_RefreshSyncState', function () {
            var _url = "/Purchase/JsonPRList?State=" + $("#PRState").val() + "&Department=" + $('#Department').val();
            $("#PRListGrid").jqGrid('setGridParam', { datatype: 'json', url: _url }).trigger("reloadGrid");
        });
        $.get('/Purchase/Service_PR_Vaild_ExportExcelForPR?prNO=' + _ids, function (msg) {
            if (msg == "ok") {
                location.href = "/Purchase/ExportExcelForPurchase?prNo=" + _ids;     
            } else if (msg.indexOf('NG') == -1) {
                var str = msg.split('|');
                alert("采购申请单[" + str[1] + "]的零件号[" + str[2] + "]ERP料号为空，请点同步ERP料号再导出申请单！");
            } else {
                alert('数据服务发生异常(或物料还未同步)，请检查数据服务器！');
            }
        }, 'html');
        
        
        //$.ajax({
        //    type: "Post",
        //    dataType: "html",
        //    url: "/Purchase/ExportExcelForPurchase?prNo=" + _ids,
        //    data: {},
        //    error: function () {

        //    },
        //    success: function (msg) {
        //        if (msg != "ok" && msg.indexOf('NG')==-1) {
        //            location.href = "/Purchase/ExportExcelForPurchase?prNo=" + _ids;
        //        }
        //        else {
        //            var str = msg.split('|');
        //            alert("采购申请单[" + str[1] + "]的零件号[" + str[2] + "]ERP料号为空，请点同步ERP料号再导出申请单！")
        //        }
        //    }
        //});
    }
    else {
        alert("请选择一条记录．");
    }

}
//Store all the grid data to PRContent and send it to controller

//TODO: 保存PR单
function CreatePR(Submit) {
    var PRContents = new Object();
    //if ($("#MoldNumber").val() != "") {
    //    PRContents["MoldNUmber"] = $("#MoldNumber").val();
    //} else {
    //    alert("请先设置模具项目！");
    //    return false;
    //}
    ////验证
    $("#CreatePR").attr("disabled", true);
    if ($("#PurchaseType").val() == 0) {
        $("#CreatePR").removeAttr("disabled");
        alert("请选择采购类型")
        return false;
    }
    if (sessionStorage['SpecKey'] != null) {
        if (sessionStorage['SpecKey'] == 'hr') {
            if ($("#ApprovalUserID").val().trim() == "0" || $("#ApprovalUserID").val().trim()=='-') {
                $("#CreatePR").removeAttr("disabled");
                alert("请选择申请人")
                return false;
            }
        }
    }    
    var m = true;
    var s = true;
    var c = true;
    var alertMsg = "";
    var rowData = $("#PRContentGrid").jqGrid("getRowData");
    for (var i = 0; i < rowData.length; i++) {
        if (sessionStorage['SpecKey'] == 'hr') {
            if (rowData[i].CostCenterID.trim() == "" || rowData[i].CostCenterID == "0") {
                alertMsg = rowData[i].JobNo + ";" + alertMsg;
                c = false;
            }
            if (rowData[i].BrandName.trim() == "" || rowData[i].BrandName == "-") {
                alertMsg = rowData[i].JobNo + ";" + alertMsg;
                m = false;
            }
        }        
        if (rowData[i].Specification.trim() == "" || rowData[i].CostCenterID == undefined) {
            alertMsg = rowData[i].JobNo + ";" + alertMsg;
            s = false;
        }      
    }
    if ($('#TaskType').val() == 0) {
        if (!m) {
            alert("零件号[" + alertMsg + "]的品牌为空，请选择品牌后再提交单据！");
            $("#CreatePR").removeAttr("disabled");
            return false;
        }
    }
    if (!s) {
        alert("零件号[" + alertMsg + "]的规格为空，请填写规格后再提交单据！");
        $("#CreatePR").removeAttr("disabled");
        return false;
    }
    if (!c) {
        alert("零件号[" + alertMsg + "]的归属部门为空，请选择归属部门后再提交单据！");
        $("#CreatePR").removeAttr("disabled");
        return false;
    }
    if (Submit) {
        $("#SubmitPR").attr("disabled", true);
    } else {
        //添加外发任务工时记录创建逻辑 michael
        var _wfFlag = true;
        if ($('#TaskType').val() > 0) {
            $.get('/Task/Service_Save_wfTaskHour', function (res) {
                if (res != '') {
                    _wfFlag = false;
                    alert('任务 ' + res + '创建工时记录时失败！')
                }
            })
        }
    }
     
    var itemData = "";
    var name = "PRContents";    
    if (rowData.length > 0) {
        for (var i = 0; i < rowData.length; i++) {
            var _purchaseDrawing = false;
            if (rowData[i].Drawing == "Yes") {
                _purchaseDrawing = true;
            }
            itemData = itemData + name + "[" + i + "].PRContentID=" + rowData[i].ID + "&" +
                    name + "[" + i + "].PartID=" + rowData[i].PartID + "&" +
                    name + "[" + i + "].TaskID=" + rowData[i].TaskID + "&" +
                    name + "[" + i + "].WarehouseStockID=" + rowData[i].WarehouseStockID + "&" +
                    name + "[" + i + "].PartName=" + rowData[i].Name + "&" +
                    name + "[" + i + "].Quantity=" + rowData[i].Quantity + "&" +
                    name + "[" + i + "].PartNumber=" + rowData[i].PartNumber + "&" +
                    name + "[" + i + "].PartSpecification=" + rowData[i].Specification + "&" +
                    name + "[" + i + "].MaterialName=" + rowData[i].Material + "&" +
                    name + "[" + i + "].Hardness=" + rowData[i].Hardness + "&" +
                    name + "[" + i + "].JobNo=" + rowData[i].JobNo + "&" +
                    name + "[" + i + "].BrandName=" + rowData[i].BrandName + "&" +
                    name + "[" + i + "].SupplierName=" + rowData[i].SupplierName + "&" +
                    name + "[" + i + "].Drawing=" + rowData[i].Drawing + "&" +
                    name + "[" + i + "].PurchaseDrawing=" + _purchaseDrawing + "&" +
                    name + "[" + i + "].Memo=" + rowData[i].Memo + "&" +
                    name + "[" + i + "].EstimatePrice=0&" +
                    name + "[" + i + "].Enabled=true" + "&" +
                    name + "[" + i + "].PurchaseItemID=" + rowData[i].PurchaseItemID + "&" +
                    name + "[" + i + "].RequireTime=" + (rowData[i].RequireTime == undefined ? "1900-1-1" : rowData[i].RequireTime) + "&" +
                    name + "[" + i + "].MoldNumber=" + rowData[i].MoldNumber + "&" +
                    name + "[" + i + "].CostCenterID=" + rowData[i].CostCenterID + "&"+
                    name + "[" + i + "].PlanQty=" + rowData[i].PlanQty + "&" +
                    name + "[" + i + "].PurchaseTypeID=" + rowData[i].PurchaseType + "&" +
                    name + "[" + i + "].Time=" + rowData[i].Time + "&" +
                    name + "[" + i + "].ERPPartID=" + rowData[i].ERPPartID + "&";
        }
        itemData = itemData + "PurchaseRequestID=" + ($("#PurchaseRequestID").val() == undefined ? 0 : $("#PurchaseRequestID").val()) +
            "&ProjectID=" + $("#ProjectID").val() + "&Memo=" + $("#PRMemo").val() +
            "&SupplierID=" + $("#SupplierID").val() + "&PurchaseType=" + $("#PurchaseType").val() + "&ApprovalERPUserID=" + $("#ApprovalUserID").val() +
            "&wsUserID=" + $("#wsUserID").val() + "&selPartModal=" + $('#_selPartModal').val();
        $.ajax({
            type: "Post",
            dataType: "html",
            url: "/Purchase/PRSave",
            data: itemData,
            error: function () {

            },
            success: function (msg) {

                if (Submit) {
                    SubmitPRCheck();
                } else {
                    alert("申请单保存成功");
                    location.href = "/Purchase/PRDetail?PurchaseRequestID=" + msg;
                }

            }
        });
    } else {
        alert("申请单内容不能为空!");
        $("#CreatePR").removeAttr("disabled");
        return false;
    }
}

function SubmitPRCheck() {
    var _selr = $("#PRContentGrid").jqGrid("getDataIDs");
    var _check = true;
    for (var i = 1; i <= _selr.length; i++) {
        var _reqDate = $("#PRContentGrid").getCell(i, "RequireTime");
        if (_reqDate == "-") {
            _check = false;
        }
    }
    if (!_check) {
        alert("请输入各个采购件需求日期")
        $("#SubmitPR").removeAttr("disabled");
    } else {
        if (confirm("确认提交采购申请？")) {
            SubmitPR($("#PurchaseRequestID").val());
        } else {
            $("#SubmitPR").removeAttr("disabled");
        }
    }
}


//Append a new PRContent to the content list without saving it to the database
function AddPRContent(row) {
    console.log($("#row").val());
    var str = '-';
    var moldNum='';
    var _selPartModal = Number($('#_selPartModal').val());
    switch (_selPartModal) {
        case 1:
            moldNum = $('#MoldNum').val();
            break;
        case 2:
            moldNum = $('#HCClass').val();
            break;
        case 5:
            moldNum = $('#BKClass').val();
            break;
    }
    var prcID = $("#PRContentID").val();
    if ( row == 0) {//$("#PRContentID").val() == 0 ||
        var _tempID = Number($("#tempID").val()) - 1;
        $("#tempID").val(_tempID);
        data = {
            ID: 0,
            Name: $("#Name").val(),
            Quantity: $("#Quantity").val(),
            PartNumber: moldNum + str + $("#JobNo").val(),
            Material: $("#MaterialID option:selected").text().trim(),
            Hardness: $("#HardnessID option:selected").text(),
            Specification: $("#Specification").val(),
            BrandName: $("#Brand option:selected").text() == "-" ? "" : $("#Brand option:selected").text(),
            //SupplierName: $("#AvailableSuppliers option:selected").text() == "-" ? "" : $("#AvailableSuppliers option:selected").text(),
            SupplierName: $('#AvailableSuppliers').val(),
            Memo: $("#Memo").val(),
            State: $("#State").val(),
            RequireTime: $("#RequireDate").val(),
            MoldNumber: moldNum,
            JobNo: $("#JobNo").val(),
            PlanQty: $('#PlanQty').val(),
            PartID: $('#PartID').val(),
        };
        $("#PRContentGrid").addRowData(_tempID, data, 0, 0);
    } else {
        var _rowno = $("#row").val();
        var Material=$("#MaterialID option:selected").text() == "-" ? " " : $("#MaterialID option:selected").text();
        var Hardness=$("#HardnessID option:selected").text() == "-" ? " " : $("#HardnessID option:selected").text();
        var BrandName = $("#Brand option:selected").text() == "-" ? " " : $("#Brand option:selected").text();
        var SupplierName = $('#AvailableSuppliers').val();
        $("#PRContentGrid").jqGrid('setCell', _rowno, 'Name', $("#Name").val());
        $("#PRContentGrid").jqGrid('setCell', _rowno, 'Quantity', $("#Quantity").val());
        $("#PRContentGrid").jqGrid('setCell', _rowno, 'Specification', $("#Specification").val());
        $("#PRContentGrid").jqGrid('setCell', _rowno, 'Material', Material);
        $("#PRContentGrid").jqGrid('setCell', _rowno, 'Hardness', Hardness);
        $("#PRContentGrid").jqGrid('setCell', _rowno, 'BrandName', BrandName);
        $("#PRContentGrid").jqGrid('setCell', _rowno, 'SupplierName', SupplierName);
        $("#PRContentGrid").jqGrid('setCell', _rowno, 'Memo', $("#Memo").val());
        $("#PRContentGrid").jqGrid('setCell', _rowno, 'EstimatePrice', $("#EstimatePrice").val());
        $("#PRContentGrid").jqGrid('setCell', _rowno, 'RequireTime', $("#RequireDate").val());
        $("#PRContentGrid").jqGrid('setCell', _rowno, 'MoldNumber', moldNum);
        $("#PRContentGrid").jqGrid('setCell', _rowno, 'State', $("#State").val());
        $("#PRContentGrid").jqGrid('setCell', _rowno, 'JobNo', $("#JobNo").val());
        $("#PRContentGrid").jqGrid('setCell', _rowno, 'PartNumber', moldNum + str + $("#JobNo").val());
        $("#PRContentGrid").jqGrid('setCell', _rowno, 'PlanQty', $("#PlanQty").val());
        $("#PRContentGrid").jqGrid('setCell', _rowno, 'PartID', $("#PartID").val());
    }
}

function showBrand(row) {
    var taskid = $("#PRContentGrid").getCell(row, "TaskID");
    if (taskid == 0) {
        $('#brandtr').show();
    }
    else
        $('#brandtr').hide();
}


function EditPrContent(id, row) {
    //外发采购不强制填写品牌 采购项taskid为0时 显示品牌
    //showBrand(row);
    $("#row").val(row);
    $("#PRContentAddLabel").html("编辑零件");
    $("#State").val($("#PRContentGrid").getCell(row, "State"));
    if (row == 0) {
        $("#PRContentAdd").modal("show");
    }
    else {       
        var moldNum;
        var jobno;
        var partNum;
        var rowdata = $('#PRContentGrid').jqGrid("getRowData", row);
        //console.log(rowdata);
        partNum = rowdata.PartNumber;
        moldNum = partNum.indexOf('-') == -1 ? '' : partNum.substr(0, partNum.indexOf('-'));
        jobno = partNum.indexOf('-') == -1 ? '' : partNum.substr(partNum.indexOf('-')+1, partNum.length - partNum.indexOf('-'));
        //var rowdata = $('#PRContentGrid').jqGrid("getRowData", row);
        //console.log(rowdata);
        if (rowdata.JobNo != '' && rowdata.JobNo != undefined) {
            $("#JobNo").val(rowdata.JobNo);
        } else {
            $("#JobNo").val(jobno);
        }

        var _selPartModal = Number($('#_selPartModal').val());
        switch (_selPartModal) {
            case 1:
                $('#MoldNum').val(rowdata.MoldNumber);
                break;
            case 2:
                $('#HCClass').val(rowdata.MoldNumber);
                break;
            case 3:
                $('#MoldNum').val(rowdata.MoldNumber);
                break;
            case 5:
                $('#BKClass').val(rowdata.MoldNumber);
                break;
        }
      
        $("#PRContentID").val(rowdata.ID);
        $("#Name ").val(rowdata.Name);
        $("#PartNumber").val(rowdata.PartNumber);
        $("#Quantity").val(rowdata.Quantity);
        $("#Specification").val(rowdata.Specification);

        $("#Memo").val(rowdata.Memo);
        $("#EstimatePrice").val(rowdata.EstimatePrice);
        //$("#RequireDate").val(rowdata.RequireTime);
        $("#MoldNumber").val(rowdata.MoldNumber);
        $("#PlanQty").val(rowdata.PlanQty);
        $("#PartID").val(rowdata.PartID);
        $("#MaterialID option").each(function () {
            if ($(this).text() == rowdata.Material) {
                //$(this).attr("selected", "true");
                var $v = $(this);
                var v = $v[0];
                v.selected = 'selected';
            }
        })

        LoadHardnessByName(rowdata.Material, rowdata.Hardness);

        var brName = rowdata.BrandName;
        LoadBrands(brName);

        //$("#AvailableSuppliers option").each(function () {
        //    var supp = $("#PRContentGrid").getCell(row, "SupplierName")
        //    if ($(this).text() == $("#PRContentGrid").getCell(row, "SupplierName")) {
        //        //$(this).attr("selected", "true");
        //        var $v = $(this);
        //        var v = $v[0];
        //        v.selected = 'selected';
        //    }
        //})
        $('#AvailableSuppliers').val(rowdata.SupplierName);
        //var _requireDate = renderDate($("#PRContentGrid").getCell(row, "RequireTime"));
        var _requireDate = rowdata.RequireTime;
        if (_requireDate!=undefined && _requireDate != null && _requireDate != '') {
            _requireDate = _requireDate == "1970-01-01" || _requireDate == "1900-01-01" ? getNowFormatDate() : _requireDate;
        } else {
            _requireDate = getNowFormatDate();
        }
        
        $("#RequireDate").val(_requireDate);
        $("#PRContentAdd").modal("show");
    }
}

function getNowFormatDate() {
    var date = new Date();
    var seperator1 = "-";
    var year = date.getFullYear();
    var month = date.getMonth() + 1;
    var strDate = date.getDate();
    if (month >= 1 && month <= 9) {
        month = "0" + month;
    }
    if (strDate >= 0 && strDate <= 9) {
        strDate = "0" + strDate;
    }
    var currentdate = year + seperator1 + month + seperator1 + strDate;
    return currentdate;
}

function LoadPR(PRID) {
    $.getJSON("/Purchase/JsonPurchaseRequest?PurchaseRequestID=" + PRID, function (msg) {
        $("#PurchaseRequestNumber").val(msg.PurchaseRequestNumber);
    });
}

function LoadProject(ProjectID) {
    $.getJSON("/Project/JsonProject?ProjectID=" + ProjectID, function (msg) {
        $("#MoldNumber").val(msg.MoldNumber);
        $("#PartNumber").val(msg.MoldNumber + "-");
        $("#ProjectID").val(msg.ProjectID);
    });
}

//打开供应商信息编辑对话框
function LoadSupplier(SupplierID) {
    $.getJSON("/Purchase/JsonSupplier?SupplierID=" + SupplierID, function (msg) {
        $("#SupplierID").val(msg.SupplierID);
        $("#Code").val(msg.Code);
        $("#Name").val(msg.Name);
        $("#FullName").val(msg.FullName);
        $("#Address").val((msg.Address == '' || msg.Address == null) ? 'X' : msg.Address);
        $("#Bank").val((msg.Bank == '' || msg.Bank == null) ? 'X' : msg.Bank);
        $("#Account").val((msg.Account == '' || msg.Account == null)  ? 'X' : msg.Account);
        $("#TaxNo").val((msg.TaxNo == '' || msg.TaxNo == null)  ? 'X' : msg.TaxNo);
        $("#TaxRate").val(msg.TaxRate);
        $("#Settlement").val((msg.Settlement == '' || msg.Settlement == null)  ? 'X' : msg.Settlement);
        $("#Enabled").val(msg.Enabled);
        $('#JianSuo').val(msg.JianSuo);
    });

    $("#SupplierEdit").modal("show");
}

//清空供应商信息对话框
function ClearSupplierDialog() {
    $("#SupplierInfo input").val("X");
    $('#TaxRate').val('0.00');
    $("#Code").val("X");
    $("#Name").val("");
    $("#FullName").val("");
    //$("#SupplierID").val(0);
    //$("#Name").val("");
    //$("#FullName").val("");
    //$("#Contact").val("");
    //$("#Email").val("");
    //$("#Enabled").val(1);
}


//function LoadSelectedSuppliersText(PurchaseRequestID) {
//    var sup = "";
//    $.getJSON("/Purchase/JsonPRSupplier?PurchaseRequestID=" + PurchaseRequestID, function (msg) {
//        $.each(msg, function (i, n) {
//            if (sup == "") {
//                sup = n.SupplierName;
//            } else {
//                sup = sup + "," + n.SupplierName;
//            }
//        });
//        $("#PRSuppliers").val(sup);
//    });   
//}






function DisplayTotal() {
    var total = $("#" + $("#AssignedSupplier option:selected").val()).html();
    $("#TotalPrice").val(total);
}

function LoadAssignedSupplier(SupplierID) {
    $.getJSON("/Purchase/JsonSupplier?SupplierID=" + SupplierID, function (msg) {
        $("#RecommandSupplierName").val(msg.Name);
        $("#SupplierName").val(msg.Name);
        $("#SupplierID").val(SupplierID);
    });
}

function GetSupplierID(SupplierName) {
    $.get('/Purchase/GetSupplierID?_sName=' + SupplierName, function (res) {
        $('#SupplierID').val(res);
    })
}

function LoadUser(UserID, FieldName) {
    $.getJSON("/User/GetUserByID?UserID=" + UserID, function (msg) {
        $("#" + FieldName).val(msg.FullName);
    })
}

function LoadBrands(BrandName) {
    $("#Brand option").remove();
    $("#Brand").append($("<option/>", {
        value: 0,
        text: "-"
    }));
    var _type = '';
    var _selPartModal = Number($('#_selPartModal').val());
    switch (_selPartModal) {
        case 1:
            _type = '模具材料';
            break;
        case 2:
            _type = '生产耗材';
            break;
        default:
            _type = '模具材料';
            break;       
    }
    if (BrandName == undefined) {
        $.getJSON("/Administrator/Service_GetBrandsByType?_type=" + _type, function (msg) {
            $.each(msg, function (i, n) {
                $("#Brand").append($("<option/>", {
                    value: n.BrandID,
                    text: n.Name
                }))
            });
        })
    } else {
        $.getJSON("/Administrator/Service_GetBrandsByType?_type=" + _type, function (msg) {
            $.each(msg, function (i, n) {
                if (BrandName == n.Name) {
                    $("#Brand").append($("<option/>", {
                        value: n.BrandID,
                        text: n.Name,
                        selected: "true"
                    }))
                } else {
                    $("#Brand").append($("<option/>", {
                        value: n.BrandID,
                        text: n.Name
                    }))
                }

            });
        })
    }
}



function RestartPR() {
    $.ajax({
        url: "/Purchase/Restart?PurchaseRequestID=" + $("#PurchaseRequestID").val() + "&Memo=" + $("#RestartMemo").val(),
        datatype: "html",
        error: function () {

        },
        success: function (msg) {
            if (msg == "1") {
                alert("申请单重启成功");
                location.reload();
            } else {
                alert("申请单重启失败");
            }
            $("#SelectSupplierDialog").modal("hide");
            location.reload();
        }
    });
}


function ValidateCreate(FormName) {
    var RequiredFieldValid = true;
    var PhaseDateValid = true;
    var errorMessage = "";

    var selector = "input.required";
    if (FormName != "") {
        selector = "#" + FormName + " :input.required";
    }

    //Required field is filled
    $(selector).each(function () {
        var item = $("#" + UnifyName(this.id));
        var _display = item.parent().css('display');
        var _hidden = item[0].hidden;
        //console.log(_display);
        //if (_display != 'none') {
        if (_hidden == false && _display != 'none') {
            if ((item.val() == "") || (item.val() == undefined)) {
                item.addClass("invalidefield");
                RequiredFieldValid = false;
            } else {
                item.removeClass("invalidefield");
            }
        }
    });
    return RequiredFieldValid;
}

function ValidateNumeric() {

}


//校验报价是否为0
function ValidateQuotation() {
    for (i = 0; i < $("input[id^='UnitPrice']").length; i++) {
        console.log($("#UnitPriceWT\\[" + i + "\\]").val());
        console.log($("#ShipDate\\[" + i + "\\]").val());
        if (Number($("#UnitPriceWT\\[" + i + "\\]").val()) == 0) {
            $("#UnitPriceWT\\[" + i + "\\]").addClass("invalidefield");
            return false;
        } else {
            $("#UnitPriceWT\\[" + i + "\\]").removeClass("invalidefield");
        }
        if ($("#ShipDate\\[" + i + "\\]").val() == '') {
            $("#ShipDate\\[" + i + "\\]").addClass("invalidefield");
            return false;
        } else {
            $("#ShipDate\\[" + i + "\\]").removeClass("invalidefield");
        }
        //if (($("#UnitPrice\\[" + i + "\\]").val() != "") && ($("#ShipDate\\[" + i + "\\]").val() == "")) {
        //    $("#ShipDate\\[" + i + "\\]").addClass("invalidefield");
        //    //alert("请输入报价零件的计划到货日期");
        //    return false;
        //} else {
        //    $("#ShipDate\\[" + i + "\\]").removeClass("invalidefield");
        //}
    }
    return true;
}

function SubmitQuotation() {
    var _data = "";
    var _name = "Quotations";
    var _unit, _total, _unitWT, _totalWT;
    for (i = 0; i < $("input[id^='UnitPriceWT']").length; i++) {
        if ($("#UnitPrice\\[" + i + "\\]").val() == "") {
            _unit = -1;
            _total = -1;
            _unitWT = -1;
            _totalWT = -1
        } else {
            _unit = $("#UnitPrice\\[" + i + "\\]").val();
            _total = $("#TotalPrice\\[" + i + "\\]").val();
            _unitWT = $("#UnitPriceWT\\[" + i + "\\]").val();
            _totalWT = $("#TotalPriceWT\\[" + i + "\\]").val();
        }
        _data = _data + _name + "[" + i + "].UnitPrice=" + _unit + "&"
            + _name + "[" + i + "].TotalPrice=" + _total + "&"
            + _name + "[" + i + "].UnitPriceWT=" + _unitWT + "&"
            + _name + "[" + i + "].TotalPriceWT=" + _totalWT + "&"
            + _name + "[" + i + "].Quantity=" + $("#Quantity\\[" + i + "\\]").val() + "&"
            + _name + "[" + i + "].QRContentID=" + $("#QRContentID\\[" + i + "\\]").val() + "&"
            + _name + "[" + i + "].ShipDate=" + $("#ShipDate\\[" + i + "\\]").val() + "&"
    }
    _data = _data + "QuotationRequestID=" + $("#QuotationRequestID").val() + "&"
        + "SupplierID=" + $("#QRSupplierList").val() + "&"
        + "TaxRate=" + $("#TaxRate").val() + "&"
        + "QuotationDate=" + $("#QuotationDate").val() + "&"
        + "ValidDate=" + $("#ValidDate").val() + "&"
        + "TaxInclude=" + $("#TaxInclude").val() + "&"
        + "ContactID=" + $("#SupplierContact").val();
    var _url = "/Purchase/SaveQuotation";
    $.ajax({
        url: _url,
        type: "Post",
        dataType: "html",
        data: _data,
        success: function (msg) {
            alert("供应商报价保存成功");
            location.href = "/Purchase/QuotationInput?QuotationRequestID=" + $("#QuotationRequestID").val();
        }
    })
}

////加载供应商列表
//function LoadSupplierList(SupplierKeyword) {
//    var ajax ="/Purchase/JsonSuppliers";
//    if (SupplierKeyword != "") {
//        ajax=ajax+"?Keyword="+SupplierKeyword;
//    }
//    $("#SupplierList option").remove();
//    $.getJSON(ajax, function (msg) {
//        $.each(msg, function (i, n) {
//            $("#SupplierList").append($("<option/>", {
//                value: n.SupplierID,
//                text: n.Name
//            }))
//        })
//    })
//}



function LoadSupplierInfo(SupplierID) {
    $.getJSON("/Purchase/JsonSupplier?SupplierID=" + SupplierID, function (msg) {

        $("#SelectedSupplierID").val(msg.SupplierID);
        $("#SupplierID").val(SupplierID);
        $("#SupplierName").html(msg.Name);
        $("#SupplierFullName").html(msg.FullName);
        var date = renderDate(msg.FirstSupply);
        if (date != "1-1-1") {
            $("#FirstSupply").html(date);
        } else {
            $("#FirstSupply").html("");
        }

    })

    //LoadContacts(SupplierID);

}





//加载供应商联系人信息
//function LoadContacts(SupplierID) {
//    $("#Contacts button").remove();
//    ajax = "/Purchase/JsonContacts?SupplierID=" + SupplierID;
//    $.getJSON(ajax, function (msg) {
//        $.each(msg, function (i, n) {
//            $("#Contacts").append("<button class=\"btn\" id=\"" + n.ContactID + "\" style=\"margin:2px\" onclick=\"EditContact(this.id)\" >" + n.FullName + "</button>");
//        })
//    })
//}

function LoadQRContacts(SupplierID) {
    $("#QRContactList option").remove();
    ajax = "/Purchase/JsonContacts?SupplierID=" + SupplierID;
    $.getJSON(ajax, function (msg) {
        $.each(msg, function (i, n) {
            $("#QRContactList").append($("<option/>", {
                text: n.FullName,
                value: n.ContactID
            }))
        })
    })
}

//打开供应商联系人编辑框
function EditContact(id) {
    ajax = "/Purchase/JsonContact?ContactID=" + id;
    $.getJSON(ajax, function (msg) {
        $("#ContactID").val(msg.ContactID);
        $("#ContactFullName").val(msg.FullName);
        $("#OrganizationID").val(msg.OrganizationID);
        $("#Enabled").val(msg.Enabled);
        $("#Email").val(msg.Email);
        $("#Telephone").val(msg.Telephone);
        $("#Mobile").val(msg.Mobile);
        $("#Memo").val(msg.Memo);
        $("#DeleteContact").removeAttr("style");
    })
    $("#SupplierContact").modal("show");
}


//显示PR处理历史
function ShowPRHistory(PurchaseRequestID) {
    ajax = "/Purchase/JsonHistory?PurchaseRequestID=" + PurchaseRequestID;
    $("#PRHistoryList option").remove();

    $.getJSON(ajax, function (msg) {

        $.each(msg, function (i, n) {
            var date = renderTime(n.RecordDate);

            //$("#PRHistoryList").append("<option/>", {
            //    value: n.ProcessRecordID,
            //    text: n.Message

            //});
            $("#PRHistoryList").append($("<option/>", {
                value: n.ProcessRecordID,
                text: renderTime(n.RecordDate) + "  " + n.Message
            }))
        })
    });
    $("#PRHistoryModal").modal("show");
}

function LoadEmail(ContactID) {
    ajax = "/Purchase/JsonContact?ContactID=" + ContactID;
    var email = "";
    $.getJSON(ajax, function (msg) {
        var receiver = $("#QRReceiver").val();
        $("#QRReceiver").val(receiver + msg.Email + ";");
    })
}

function MailResult() {
    $.ajax({
        url: "/Purchase/SendQR?QuotationRequestID=" + $("#QuotationRequestID").val() + "&QRReceiver=" + $("#QRReceiver").val(),
        datatype: "html",
        error: function () {

        },
        success: function (msg) {
            var a = "aaa";

            if (msg.toLowerCase() == "true") {
                alert("询价单发送完成");
                location.reload();
            } else {
                alert("询价单发送失败");
            }
            $("#SelectSupplierDialog").modal("hide");
            location.reload();
        }
    });
}

function ValidateZero(FieldName) {
    if ($("#" + FieldName).val() > 0) {
        return true;
    } else {
        $("#" + FieldName).addClass("invalidefield");
        return false;
    }
}

function DeleteSupplier(id) {
    $.ajax({
        dataType: "html",
        url: "/Purchase/DeleteSupplier?SupplierID=" + id,
        error: function () { },
        success: function (msg) {
            if (msg == "") {
                location.href = "/Purchase/Suppliers";
            } else {
                alert(msg);
            }
        }
    })
}

function ClearPRContent() {
    $("#Name").val("");
    $("#PartNumber").val("");
    $("#PRContentID").val(0);
    $("#Quantity").val("");
    $("#Specification").val("");
    $("#MaterialID").val(0);
    $("#Hardness").val(0);
    $("#BrandID").val(0);
    $("#Memo").val("");
    $("#EstimatePrice").val("");
    $("#MoldNumber").val("");
}

function ClearQRContent() {
    $("#Name").val("");
    $("#PartNumber").val("");
    $("#QRContentID").val(0);
    $("#Quantity").val("");
    $("#Specification").val("");
    $("#MaterialID").val(0);
    $("#Hardness").val(0);
    $("#BrandID").val(0);
    $("#Memo").val("");
}

function SubmitPR(id, memo) {
    $.ajax({
        dataType: "html",
        url: "/Purchase/SubmitPR?PurchaseRequestID=" + id,
        error: function () { },
        success: function (msg) {
            if (msg == "") {
                alert("申请单提交成功");
                location.href = "/Purchase/Index";
            } else {
                alert(msg);
            }
        }
    });
}

function ReviewPR(id, memo) {
    $.ajax({
        dataType: "html",
        url: "/Purchase/ReviewPR?PurchaseRequestID=" + id + "&ResponseType=" + $("#ReviewResponse").val() + "&Memo=" + memo,
        success: function (msg) {
            if (msg == "") {
                alert("申请单审核完成");
                location.href = "/Purchase/Index";
            } else {
                alert(msg);
            }
        }

    })
}

function CancelPR(id) {
    $.ajax({
        dataType: 'html',
        url: "/Purchase/CancelPR?PurchaseRequestID=" + id,
        success: function (msg) {
            if (msg == "") {
                alert("申请单已取消");
                location.href = "/Purchase/Index";
            } else {
                alert(msg);
            }
        }
    })
}

function CreateQR() {
    var ids = GetMultiSelectedIDs("PRContentGrid");
    if (ids == "") {
        alert("请至少选择一个零件");
    } else {
        location.href = "/Purchase/QRDetail?PurchaseRequestID=" + $("#PurchaseRequestID").val() + "&PRContentIDs=" + ids;
    }
}

function SaveQR() {
    //获取单元格(第一个默认隐藏)
    var firsttdobj = $('#QRContentGrid td:eq(18)');
    //模拟单元格点击事件
    firsttdobj.trigger("click");

    $("#SaveQR").attr("disabled", true);
    var QRContents = new Object();
    QRContents["PurchaseRequestID"] = $("#PurchaseRequestID").val();
    QRContents["ProjectID"] = $("#ProjectID").val();

    var rowData = $("#QRContentGrid").jqGrid("getRowData");
    var itemData = "";
    var name = "QRContents";

    if (rowData.length > 0) {
        for (var i = 0; i < rowData.length; i++) {
            itemData = itemData + name + "[" + i + "].QRContentID=" + rowData[i].QRContentID + "&" +
                       name + "[" + i + "].PartName=" + rowData[i].PartName + "&" +
                       name + "[" + i + "].PartNumber=" + rowData[i].PartNumber + "&" +
                       name + "[" + i + "].Quantity=" + rowData[i].Quantity + "&" +
                       name + "[" + i + "].MaterialName=" + rowData[i].MaterialName + "&" +
                       name + "[" + i + "].PartSpecification=" + rowData[i].PartSpecification + "&" +
                       name + "[" + i + "].Hardness=" + rowData[i].Hardness + "&" +
                       name + "[" + i + "].BrandName=" + rowData[i].BrandName + "&" +
                       name + "[" + i + "].PurchaseDrawing=" + rowData[i].PurchaseDrawing + "&" +
                       name + "[" + i + "].PRContentID=" + rowData[i].PRContentID + "&" +
                       name + "[" + i + "].PurchaseItemID=" + rowData[i].PurchaseItemID + "&" +
                       name + "[" + i + "].QRcMemo=" + rowData[i].QRcMemo + "&" +
                       name + "[" + i + "].Memo=" + rowData[i].Memo + "&" +
                       name + "[" + i + "].unit=" + rowData[i].unit + "&" +
                       name + "[" + i + "].RequireDate=" + rowData[i].RequireDate + "&";

        }
        itemData = itemData + "QuotationRequestID=" + $("#QuotationRequestID").val() +
            "&PurchaseRequestID=" + $("#PurchaseRequestID").val() + "&DueDate=" + $("#DueDate").val() + "&Memo=" + $("#Memo").val();
        if ($("#ProjectID").val() != undefined) {
            itemData = itemData + "&ProjectID=" + $("#ProjectID").val();
        }
        $.ajax({
            type: "Post",
            dataType: "html",
            url: "/Purchase/QRSave",
            data: itemData,
            success: function (msg) {
                alert("询价单保存成功");
                location.href = "/Purchase/QRDetail?QuotationRequestID=" + msg;
                $("#SaveQR").removeAttr("disabled");
            }
        })
    } else {
        alert("询价单内容不能为空");
        return false;
    }
}

function CancelQR() {
    if (confirm("确认取消询价单？")) {
        location.href = "/Purchase/CancelQR?QuotationRequestID=" + $("#QuotationRequestID").val();
    }
}

function DeleteQRContent() {
    var selrows = $("#QRContentGrid").jqGrid('getGridParam', 'selarrrow');
    if (selrows.length > 0) {
        if (confirm("确认删除采购零件？")) {

            while (selrows.length > 0) {
                var _id = $("#QRContentGrid").getCell(selrows[0], "QRContentID");
                if (_id > 0) {
                    $.ajax({
                        datatype: "html",
                        url: "/Purchase/DeleteQRContent?QRContentID=" + _id,
                        error: function () {

                        },
                        success: function (msg) {

                        }
                    });
                }
                $("#QRContentGrid").delRowData(selrows[0]);
            }
        }
    } else {
        alert("请选择至少一个零件");
    }
}

function SelectQRSupplier() {
    var _qrID = $("#QuotationRequestID").val();
    LoadSupplierGroup(_qrID);
    setTimeout(LoadQRSupplierGroups(_qrID, "QGroupList"), 100);
    $("#SelectSupplierDialog").modal("show");
}

function LoadSuppliers(QRID, ZeroLine) {
    $("#AvailableSuppliers option").remove();
    if (ZeroLine == 1) {
        $("#AvailableSuppliers").append($("<option/>", { value: 0, text: "-" }));
    }

    $.getJSON("/Purchase/JsonSuppliers?QuotationRequestID=" + QRID, function (msg) {
        $.each(msg, function (i, n) {
            $("#AvailableSuppliers").append($("<option/>", {
                value: n.SupplierID,
                text: n.Name
            }))
        })
    })
}

function LoadSupplierGroup(QRID, ZeroLine) {
    $("#AvailableQRGroups option").remove();
    if (ZeroLine == 1) {
        $("#AvailableQRGroups").append($("<option/>", { value: 0, text: "-" }));
    }

    $.getJSON("/Purchase/Service_GetSupplierGroup?QuotationRequestID=" + QRID, function (msg) {
        $.each(msg, function (i, n) {
            $("#AvailableQRGroups").append($("<option/>", {
                value: n.ID,
                text: n.GroupName
            }))
        })
    })
}

function LoadQRSuppliers(QuotationRequestID, ListName, SelectedValue) {
    $("#" + ListName + " option").remove();
    var id = "";
    var _selID;
    $.getJSON("/Purchase/Service_QR_GetQRSuppliers?quotationID=" + QuotationRequestID, function (msg) {//JsonQRSupplier
        $.each(msg, function (i, n) {
            if (i == 0) {
                _selID = n.SupplierID;
            }
            $("#" + ListName).append($("<option/>", {
                value: n.SupplierID,
                text: n.SupplierName
            }))
            id = id + "," + n.SupplierID;
        })
    });
    return _selID;
}

function LoadQRSupplierGroups(QuotationRequestID, ListName) {
    $("#" + ListName + " option").remove();
    var id = "";
    var _selID;
    $.getJSON("/Purchase/Service_GetQrSupplierGroup?QuotationRequestID=" + QuotationRequestID, function (msg) {
        $.each(msg, function (i, n) {
            if (i == 0) {
                _selID = n.ID;
            }
            $("#" + ListName).append($("<option/>", {
                value: n.ID,
                text: n.GroupName
            }))
            id = id + "," + n.SupplierID;
        })
    });
    return _selID;
}

function SaveQRSuppliers() {
    if ($("#SupplierList option").length > 0) {
        var _suppliers = new Object;
        _suppliers["QuotationID"] = $("#QuotationRequestID").val();
        var i = 0;
        $("#SupplierList option").map(function () {
            _suppliers["Supplier[" + i + "].SupplierID"] = $(this).val();
            _suppliers["Supplier[" + i + "].SupplierName"] = $(this).text();
            _suppliers["Supplier[" + i + "].QuotationRequestID"] = $("#QuotationRequestID").val();
            i = i + 1;
        })

        $.ajax({
            type: "Post",
            dataType: "html",
            data: _suppliers,
            url: "/Purchase/SelectQRSupplier",
            success: function () {
                alert("供应商选择完成");
                $("#SelectSupplierDialog").modal("hide");
                LoadSupplierNames($("#QuotationRequestID").val());
            }
        })

        //var _supplierNames = "";
        //$("#SupplierList option").each(function(){
        //    _supplierNames = _supplierNames == "" ? $(this).text() : _supplierNames + "," + $(this).text();
        //})
        //$("#ExistingSuppliers").val(_supplierNames);

    } else {
        alert("请至少选择一家供应商");
    }
}

function LoadSupplierNames(QuotationRequestID) {
    $('#ExistingSuppliers').empty();
    $.ajax({
        type: "Get",
        dataType: "json",
        url: "/Purchase/Service_GetQrSupplierGroup?QuotationRequestID=" + QuotationRequestID,//Service_QRSuppliers
        success: function (msg) {
            //$("#ExistingSuppliers").val(msg);
            //var _suppliers = msg.split(',');
            var _fSel = '';
            var _fval = '';
            $.each(msg, function (i, n) {
                _fSel = _fSel + n.GroupName + ',';
                _fval = _fval + n.ID + '|';
                //if (i == 0) {
                //    _fval = n.ID;
                //}
            });
            
            _fSel = _fSel.substr(0, _fSel.length - 1);
            _fval = _fval.substr(0, _fval.length - 1);
            console.log(_fval);
            $("#ExistingSuppliers").append($('<option/>', {
                value: _fval,
                text: _fSel,
                //disabled: true,
                selected:true,
            }));
            if (msg.length > 1) {
                $.each(msg, function (i, n) {
                    $("#ExistingSuppliers").append($('<option/>', {
                        value: n.ID,
                        text: n.GroupName,
                    }));
                })
            }
            ////
            var mailBtn = $('#QREmail')[0];
            if (mailBtn != null) {
                $('#QREmail').remove();               
            }
            var _qrID = $('#QuotationRequestID').val();
            var _html = '<a id="QREmail" class="btn btn-primary" href="' + FormatHrefStr(_fval) + '" onclick="ShowDialog()" ><span class="glyphicon glyphicon-envelope"></span> 发送邮件</a>';//href=MoldSysPlugin:'+res+_qrID +'
            $('#td_GenerateLink').append(_html);
        }
    })
}


function SaveSupplierInfo() {
    if ($("#SupplierID").val() > 0) {
        $("#SupplierInfo").submit();
    } else {
        $.ajax({
            type: "Get",
            dataType: "html",
            url: "/Purchase/UniqueSupplier?Name=" + $("#Name").val(),
            success: function (msg) {
                if (msg == 0) {
                    $("#SupplierInfo").submit();
                } else {
                    alert("供应商已存在");
                }
            }
        })
    }
}

function AddQRContent() {
    if ($("#QRContentID").val() == 0) {
        var _tempID = Number($("#tempID").val()) - 1;
        $("#tempID").val(_tempID);
        data = {
            ID: _tempID,
            PartName: $("#Name").val(),
            Quantity: $("#Quantity").val(),
            PartNumber: "",
            MaterialName: $("#MaterialID option:selected").text().trim(),
            Hardness: $("#HardnessID option:selected").text(),
            PartSpecification: $("#Specification").val(),
            BrandName: $("#Brand option:selected").text(),
        };
        $("#QRContentGrid").addRowData(_tempID, data, 0, 0);
    } else {
        var _rowno = $("#row").val();
        $("#QRContentGrid").jqGrid('setCell', _rowno, 'DrawingName', $("#Name").val());
        $("#QRContentGrid").jqGrid('setCell', _rowno, 'Quantity', $("#Quantity").val());
        $("#QRContentGrid").jqGrid('setCell', _rowno, 'Specification', $("#Specification").val());
        $("#QRContentGrid").jqGrid('setCell', _rowno, 'Material', $("#MaterialID option:selected").text());
        $("#QRContentGrid").jqGrid('setCell', _rowno, 'Hardness', $("#Hardness option:selected").text());
        $("#QRContentGrid").jqGrid('setCell', _rowno, 'JobNumber', $("#BrandID").text());
        $("#QRContentGrid").jqGrid('setCell', _rowno, 'Memo', $("#Memo").val());
    }
}


function CloseQR() {
    var _id = $("#QuotationRequestID").val();
    if (confirm("确认关闭询价单？")) {
        $.ajax({
            url: "/Purchase/CloseQR?QuotationRequestID=" + _id,
            dataType: "html",
            success: function (msg) {
                if (msg == "") {
                    alert("询价单已关闭");
                } else {
                    alert(msg);
                }
                location.reload();
            }
        });
    }
}


function LoadSupplierName(FieldName, SupplierID) {
    $.getJSON("/Purchase/JsonSupplier?SupplierID=" + SupplierID, function (msg) {
        $("#" + FieldName).val(msg.FullName);
    })

}


function DeletePOContent() {
    var selrows = $("#POContentGrid").jqGrid('getGridParam', 'selarrrow');
    if (selrows.length > 0) {
        if (confirm("确认删除零件？")) {
            if (selrows.length > 0) {
                for (var i = 0; i < selrows.length; i++) {
                    var _id = $("#POContentGrid").getCell(selrows[i], "POContentID");
                    if (_id > 0) {
                        $.ajax({
                            dataType: "html",
                            url: "/Purchase/DeletePOContent?POContentID=" + _id,
                            async: false,
                        })
                    }
                }
            }
        }
        location.href = '/Purchase/PODetail?PurchaseOrderID=' + $('#PurchaseOrderID').val();
    } else {
        alert("请至少选择一个零件");
    }
}

function SubmitPO() {
    var _id = $("#PurchaseOrderID").val();
    if (confirm("确认提交订单？")) {
        $.ajax({
            dataType: "html",
            url: "/Purchase/SubmitPO?PurchaseOrderID=" + _id,
            success: function () {
                location.reload();
            }
        })
    }
}

function ReviewPO() {
    var _id = $("#PurchaseOrderID").val();
    var _memo = $("#ReviewMemo").val();
    $.ajax({
        dataType: "html",
        url: "/Purchase/ReviewPO?PurchaseOrderID=" + _id + "&ResponseType=" + $("#ReviewResponse").val() + "&Memo=" + _memo,
        success: function (msg) {
            if (msg == "") {
                alert("订单审核完成");
                location.href = "/Purchase/PurchaseOrderList";
            } else {
                alert(msg);
            }
        }

    })
}

function ValidateQR() {
    if ($("#DueDate").val() == "") {
        $("#DueDate").addClass("invalidefield")
        alert("请输入需求日期");
        return false;
    } else {
        return true;
    }
}

function RestartQR() {
    location.href = "/Purchase/RestartQuotation?QuotationRequestID=" + $("#QuotationRequestID").val();
}

function ModifyQRContentQty(ID) {

    $.getJSON("/Purchase/JsonQRContent?QRContentID=" + ID, function (msg) {
        $("#QRContentIDQty").val(msg.QRContentID);
        $("#QRContentQty").val(msg.Quantity);
        $("#QRContentName").val(msg.PartName);
    })
    $("#ModiyfContentQtyDialog").modal("show");
}

function SaveQRContentQty() {
    $.ajax({
        dataType: "html",
        type: "Get",
        url: "/Purchase/ModifyQRContentQty?QRContentID=" + $("#QRContentIDQty").val() + "&Quantity=" + $("#QRContentQty").val(),
        success: function () {
            alert("零件数量修改完成");
            location.reload();
        }
    })
}

function SendPO() {

    //$.ajax({
    //    dataType: "html",
    //    type: "Get",
    //    url:"/Purchase/"
    //})
    var _url = "/Purchase/POFormPDF?PurchaseOrderID=" + $("#PurchaseOrderID").val();
    window.open(_url);
    //location.href = _url;
    setTimeout("location.reload()", 2000);

}

function SendQR() {
    if ($("#ExistingSuppliers").val() != "") {
        var _url = "/Purchase/QRFormPDF?PurchaseRequestID=" + $("#QuotationRequestID").val();
        //location.href = _url;
        window.open(_url);
        setTimeout("location.reload()", 2000);
    } else {
        alert("请先选择询价供应商");
    }

    //location.reload();
}

function BatchApprove(PRIDs) {
    $.ajax({
        datatype: "html",
        type: "Get",
        url: "/Purchase/BatchReviewPR?PurchaseRequestIDs=" + PRIDs,
        success: function () {
            alert("批量审批完成");
            location.reload();
        }
    })
}

function LoadRecommandSuppliers(SupplierID) {
    var ajax = "/Purchase/JsonSuppliers";

    $("#SupplierID option").remove();
    $.getJSON(ajax, function (msg) {
        $("#SupplierID").append($("<option/>", {
            value: 0,
            text: '-'
        }))
        $.each(msg, function (i, n) {
            if (SupplierID == n.SupplierID) {
                $("#SupplierID").append($("<option/>", {
                    value: n.SupplierID,
                    text: n.Name,
                    selected: true
                }))
            } else {
                $("#SupplierID").append($("<option/>", {
                    value: n.SupplierID,
                    text: n.Name
                }))
            }

        })
    })
}

function ExportStdData() {
    var count = Number($("#QRContentGrid").getGridParam("reccount"));
    var data = "";


    for (i = 1; i <= count; i++) {

        data = data + $("#QRContentGrid").getCell(i, "PartNumber") + "\t" +
            $("#QRContentGrid").getCell(i, "PartSpecification") + "\t" +
            $("#QRContentGrid").getCell(i, "Quantity") + "\n";
    }
    $("#QRContentInfo").val(data);
    $("#STDDialog").modal("show");
}

function SupplierContacts() {
    var selr = $('#SupplierGrid').jqGrid('getGridParam', 'selarrrow');
    if (selr.length == 1) {
        id = $("#SupplierGrid").getCell(selr[0], "SupplierID");

        $("#SelectedSupplier").val(id);
        $("#ContactList option").remove();
        $.getJSON("/Purchase/JsonContacts?SupplierID=" + id, function (msg) {
            $.each(msg, function (i, n) {
                $("#ContactList").append($("<option/>", {
                    text: n.FullName,
                    value: n.ContactID
                }))
            })
        })
        $("#SupplierContact").modal("show");
    } else {
        alert("请选择一家供应商");
    }
}

function LoadContactInfo() {
    var _url = "/Purchase/JsonContact?ContactID=" + $("#ContactList").val();
    $("#ContactInfo :input").removeAttr("disabled");
    $.getJSON(_url, function (msg) {
        $("#ContactFullName").val(msg.FullName);
        $("#ContactID").val(msg.ContactID);
        $("#ContactType").val(msg.ContactType);
        $("#OrganizationID").val(msg.OrganizationID);
        $("#Enabled").val(msg.Enabled);
        $("#Email").val(msg.Email);
        $("#Telephone").val(msg.Telephone);
        $("#Mobile").val(msg.Mobile);
        $("#Memo").val(msg.Memo);
    })
}

function AddContact() {
    $("#ContactInfo :input").removeAttr("disabled");
    $("#ContactInfo :input").val("");
    $("#OrganizationID").val($("#SelectedSupplier").val());
    $("#ContactType").val(1);
    $("#ContactID").val(0);
    $("#Enabled").val(true);

}

function SaveContact() {
    if (ValidateEmail("Email")) {
        //$("#ContactInfo").submit();
        $.ajax({
            type: "Post",
            dataType: "html",
            data: $("#ContactInfo").serialize(),
            url: "/Purchase/SupplierContact",
            success: function () {
                alert("联系人信息已保存");
                SupplierContacts();
            }
        })
    }
}

function ResetContactDialog() {
    $("#ContactInfo :input").attr("disabled", "true");
    $("#ContactInfo :input").val("");
    $("#Enabled").val(true);
}

function DelContact() {
    if ($("#ContactList").val() == null) {
        alert("请先选择联系人");
    } else {
        if (confirm("确认删除供应商联系人？")) {
            _url = "/Purchase/DeleteContact?ContactID=" + $("#ContactList").val();
            $.ajax({
                type: "Get",
                dataType: "html",
                url: _url,
                success: function () {
                    alert("联系人已删除");
                    SupplierContacts();
                }
            })
        }
    }
}


function LoadMoldNumbers(Keyword) {
    var _url = "";
    $("#MoldList option").remove();
    if (Keyword != undefined) {
        _url = "/Project/JsonMoldNumber?Keyword=" + Keyword;
    } else {
        _url = "/Project/JsonMoldNumber"
    }
    $.getJSON(_url, function (msg) {
        $.each(msg, function (i, n) {
            $("#MoldList").append($("<option/>", {
                value: n,
                text: n
            }))
        });
    })
}

function TransferPO() {
    //获取所有行PurchaseItemId
    var rowids = $('#POContentGrid').jqGrid("getDataIDs");
    if (rowids.length > 0) {
        var itemIds = '';
        var purType = '';
        for (var i = 0; i < rowids.length; i++) {
            var rowData = $("#POContentGrid").jqGrid('getRowData', rowids[i]);
            itemIds = itemIds + rowData.PurchaseItemID + ',';
            purType=rowData.PurchaseType;
        }
        itemIds = itemIds.substr(0, itemIds.length - 1);
        location.href = "/Purchase/PODetail?PurchaseOrderID=0&itemIds=" + itemIds + '&purType=' + purType;
    } else {
        alert('失败：右侧表格无数据行.请从左侧列表选择采购项添加至右侧表格！');
        return false;
    }
}

//TODO:生成采购订单
function CreatePO() {
    var POContents = new Object();
    $("#CreatePO").attr("disabled", true);
    var itemData = "";
    var name = "POContents";
    var _url = "/Purchase/CreatePurchaseOrder";
    var validate = true;

    var _gridRows = $("#POContentGrid").jqGrid("getDataIDs");
    console.log(_gridRows);
    for (i = 0; i <= _gridRows.length ; i++) {
        $('#POContentGrid').jqGrid('saveRow', _gridRows[i]);
    }

    var rowData = $("#POContentGrid").jqGrid("getRowData");
    if (rowData.length > 0) {
        if ($('#SupplierName').val() == null || $('#SupplierName').val() == '' || $('#SupplierName').val() == undefined) {
            $("#SaveOrder").removeAttr("disabled");
            alert('请选择供应商！');
            return;
        }
        ////保存订单——
        
        //
        for (var i = 0; i < rowData.length; i++) {
            console.log(rowData[i]);
            if ((rowData[i].Quantity == 0) || (rowData[i].UnitPriceWT == 0) || (rowData[i].TotalPriceWT == 0) || (rowData[i].DeliverDate == "" || rowData[i].DeliverDate == "-")) {
                alert("请输入订单项数量/单价/总价/交付日期信息");
                $('#POContentGrid').jqGrid('editRow', i, true);
                $("#SaveOrder").removeAttr("disabled");
                return false;
            } else {
                itemData = itemData + name + "[" + i + "].PurchaseItemID=" + rowData[i].PurchaseItemID + "&" +
                    name + "[" + i + "].Quantity=" + rowData[i].Quantity + "&" +
                    name + "[" + i + "].UnitPrice=" + rowData[i].UnitPrice + "&" +
                    name + "[" + i + "].TotalPrice=" + rowData[i].TotalPrice + "&" +
                    name + "[" + i + "].UnitPriceWT=" + rowData[i].UnitPriceWT + "&" +
                    name + "[" + i + "].TotalPriceWT=" + rowData[i].TotalPriceWT + "&" +
                    name + "[" + i + "].PlanTime=" + rowData[i].RequestTime + "&" +
                    name + "[" + i + "].Time=" + rowData[i].Time + "&" +
                name + "[" + i + "].Memo=" + rowData[i].Memo + "&";
            }
        }
        
        itemData = itemData +//$("#Supplier option:selected").val() +
            "&Currency=" + $("#Currency").val() +
            "&TaxRate=" + $("#TaxRate").val() +
            "&PurchaseType=" + $("#PurchaseTypeID").val() +
            "&SupplierName=" + $("#SupplierName").val() +
            "&POMemo=" + $('#Memo').val();//$("#Supplier option:selected").text();
        $("#SaveOrder").removeAttr("disabled");
        $.ajax({
            type: "Post",
            dataType: "html",
            url: _url,
            data: itemData,
            error: function () {

            },
            success: function (msg) {
                if (msg == "") {
                    //alert("订单生成");
                    $("#SaveOrder").attr("disabled", false);
                    location.href = "/Purchase/PurchaseOrderList";
                } else {
                    alert(msg);
                }
            }
        });
    } else {
        alert("当前订单中不包含任何采购项");
    }
}


function PickSupplier() {
    var _supID = $("#AvailableQRGroups option:selected").val();
    var _supName = $("#AvailableQRGroups option:selected").text();
    $("#AvailableQRGroups option:selected").remove();
    if (_supName != "") {
        $("#QGroupList").append($("<option/>", {
            value: _supID,
            text: _supName
        }));
    }
}

function RemoveSupplier() {
    var _supID = $("#QGroupList option:selected").val();
    var _supName = $("#QGroupList option:selected").text()
    $("#QGroupList option:selected").remove();
    if (_supName != "") {
        $("#AvailableQRGroups").append($("<option/>", {
            value: _supID,
            text: _supName
        }));
    }
}


function DeletePRContent() {
    if (confirm("确认删除选中采购申请内容?")) {
        var selrows = $("#PRContentGrid").jqGrid('getGridParam', 'selarrrow');
        var _rowcount = selrows.length;
        var _selrows = selrows;
        var _ids = "";

        //for (i = 0; i < selrows.length; i++) {
        //    //console.log(selrows);
        //    $("#PRContentGrid").delRowData(selrows[0]);

        //}


        ////Delete not saved items
        for (i = _rowcount - 1; i >= 0 ; i--) {

            var _id = $("#PRContentGrid").getCell(selrows[i], "ID");
            if (_id != "") {
                _ids = _ids == "" ? _id : _ids + "," + _id;
            } else {
                $("#PRContentGrid").delRowData(selrows[0]);
            }
        }

        if (_ids != "") {
            var _url = "/Purchase/DeletePRContent?PRContentIDs=" + _ids + "&PurchaseRequestID=" + $("#PurchaseRequestID").val();
            $.ajax({
                url: _url,
                type: "Get",
                success: function (msg) {
                    var _count = Number(msg);

                    if (_count != NaN) {
                        if (_count == 0) {
                            alert("当前申请单零件已全部删除，申请单失效");
                            PRContentGrid("", $("#PurchaseRequestID").val(), "", "");
                            location.href = "/Purchase/Index";
                        } else if (_count > 0) {
                            alert("零件删除成功");
                            location.reload();
                        }
                    }
                    else {
                        alert("零件" + msg + "订单已发出，无法删除");
                    }
                }
            })
        }


    }
}

function getQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return null;
}

function SupplierListImport(_suplistID,isWF) {
    var js = '';
    $('#' + _suplistID).html('');
    $.get('/Purchase/JsonSuppliersByJS?js=' + js, function (res) {
        console.log(typeof (res));
        var fo = res.toString();
        fo = fo.split(",");
        for (var i = 0; i < fo.length; i++) {
            //中文
            var v1 = fo[i].split('/')[0];
            //英文
            var v2 = fo[i].split('/')[1];
            //应对英文显示bug
            if (v1 == v2) {
                v2 = v2.substr(0,3);
            }
            var ohtml = "<option value='" + v1 + "'>" + v2 + "</option>";
            var $ohtml = $(ohtml);
            $('#' + _suplistID).append($ohtml);
        }
    });
}

//调整采购项计划日期
function PurItenChangePlan(purItemID, planDate) {
    console.log(purItemID);
    console.log(planDate);
    if (Number(purItemID) > 0) {
        $.get('/Purchase/Service_PurItem_ChangePlan?purchaseItemID=' + purItemID + '&planDate=' + planDate, function () { });
    }    
}

function BindRowAction(id, iCol) {
    //keyup
    $("#" + id + "_Quantity").on("keyup", function () {
        UpdateTotal(id);
    })

    $("#" + id + "_UnitPrice").on("keyup", function () {
        UpdateUnitWT(id);
    })

    $("#" + id + "_UnitPriceWT").on("keyup", function () {
        UpdateUnit(id);
    })

    $("#" + id + "_Time").on("keyup", function () {
        UpdateTotal(id);
    })
    //focus
    //$("#" + id + "_Quantity").on("focus", function () {
    //    $("#ActiveCol").val(4);
    //    //$("#" + id + "_Quantity").attr("type", "number");
    //})

    //$("#" + id + "_UnitPrice").on("focus", function () {
    //    $("#ActiveCol").val(5);
    //    //$("#" + id + "_UnitPriceWT").attr("type", "number");
    //})

    //$("#" + id + "_UnitPriceWT").on("focus", function () {
    //    $("#ActiveCol").val(6);
    //    //$("#" + id + "_UnitPriceWT").attr("type", "number");
    //})

    //UpdateUnit
    $("#" + id + "_TotalPriceWT").on("keyup", function () {
        UpdateUnitByTotalWT(id);
    })

    $("#" + id + "_TotalPrice").on("keyup", function () {
        UpdateUnitByTotal(id);
    })
    //$("#" + id + "_TotalPriceWT").on("focus", function () {
    //    $("#ActiveCol").val(7);
    //    //$("#" + id + "_TotalPriceWT").attr("type", "number");
    //})

    
    //if (iCol == undefined) {
    //    iCol = Number($("#ActiveCol").val());
    //}

    //switch (iCol) {
    //    case 4:
    //        _inputID = "#" + id + "_Quantity";
    //        break;
    //    case 5:
    //        _inputID = "#" + id + "_UnitPriceWT";
    //        break;
    //    case 6:
    //        _inputID = "#" + id + "_TotalPriceWT";
    //        break;
    //    default:
    //        _inputID = "#" + id + "_Quantity";
    //        break;
    //}

    //setTimeout("$(_inputID).focus()", 1);
    //$(_inputID).select();
}

//更新含税单价
function UpdateUnitWT(id) {
    //更新含税单价
    var _taxRate = $('#TaxRate').val();
    $("#" + id + "_UnitPriceWT").val((Number($("#" + id + "_UnitPrice").val()) * (1+Number(_taxRate)/100)).toFixed(2));
    //更新总价
    UpdateTotal(id);
}

//更新未税单价
function UpdateUnit(id) {
    //更新未税单价
    var _taxRate = $('#TaxRate').val();
    $("#" + id + "_UnitPrice").val((Number($("#" + id + "_UnitPriceWT").val()) / (1 + Number(_taxRate) / 100)).toFixed(2));
    //更新总价
    UpdateTotal(id);
}

//更新总价
function UpdateTotal(id) {
    //console.log('更新总价——');
    //console.log('数量:' + $("#" + id + "_Quantity").val());
    //console.log('单价:' + $("#" + id + "_UnitPriceWT").val());
    $("#" + id + "_TotalPriceWT").val((Number($("#" + id + "_Time").val()) * Number($("#" + id + "_Quantity").val()) * Number($("#" + id + "_UnitPriceWT").val())).toFixed(2));
    $("#" + id + "_TotalPrice").val((Number($("#" + id + "_Time").val()) * Number($("#" + id + "_Quantity").val()) * Number($("#" + id + "_UnitPrice").val())).toFixed(2));
}

//更新单价
function UpdateUnitByTotalWT(id) {
    //console.log('更新单价：');
    //console.log('总价:'+$("#" + id + "_TotalPriceWT").val());
    //console.log('数量:' + $("#" + id + "_Quantity").val());

    //更新含税单价
    $("#" + id + "_UnitPriceWT").val((Number($("#" + id + "_TotalPriceWT").val()) / Number($("#" + id + "_Quantity").val()) / Number($("#" + id + "_Time").val())).toFixed(2));
    //更新未税单价
    var _taxRate = $('#TaxRate').val();
    $("#" + id + "_UnitPrice").val(((Number($("#" + id + "_TotalPriceWT").val()) / Number($("#" + id + "_Quantity").val()) / Number($("#" + id + "_Time").val())) / (1 + Number(_taxRate) / 100)).toFixed(2));

    $("#" + id + "_TotalPrice").val((Number($("#" + id + "_TotalPriceWT").val()) / (1 + Number(_taxRate) / 100)).toFixed(2));
}
function UpdateUnitByTotal(id) {
    //console.log('更新单价：');
    //console.log('总价:'+$("#" + id + "_TotalPriceWT").val());
    //console.log('数量:' + $("#" + id + "_Quantity").val());
    var _taxRate = $('#TaxRate').val();
    //更新含税单价
    $("#" + id + "_UnitPriceWT").val((Number($("#" + id + "_TotalPrice").val()) * (1 + Number(_taxRate) / 100) / Number($("#" + id + "_Quantity").val()) / Number($("#" + id + "_Time").val())).toFixed(2));
    //更新未税单价   
    $("#" + id + "_UnitPrice").val(((Number($("#" + id + "_TotalPrice").val()) / Number($("#" + id + "_Quantity").val()) / Number($("#" + id + "_Time").val()))).toFixed(2));

    $("#" + id + "_TotalPriceWT").val((Number($("#" + id + "_TotalPrice").val()) * (1 + Number(_taxRate) / 100)).toFixed(2));
}