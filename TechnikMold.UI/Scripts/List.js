$("document").ready(function () {

    $("#DisableContent").on("click", function () {
        
        if (ValidateSelected("ListContent")){
            if (confirm("确认删除\"" + $("#ListContent option:selected").text()+"\"?")) {
                location.href = "/List/DeleteListContent?ListValueID="+$("#ListContent option:selected").val();
            }
        } 
    })


    $("#EditListContent").on("click", function () {
        if (ValidateSelected("ListContent")) {
            $("#ListTypeID").val($("#ListType option:selected").val());
            $("#Name").val($("#ListContent option:selected").text());
            $("#ListValueID").val($("#ListContent option:selected").val());
        }
        $("#ListContentModal").modal("show");
    })
})

function ValidateSelected(SelectName){
    var selItem = $("#"+SelectName+" option:selected");
    if (selItem.length>0){
        return true;
    } else {
        alert("请先从列表中选择一项");
        return false;
    }
}
