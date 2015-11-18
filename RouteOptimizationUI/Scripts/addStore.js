
    function EditStoreList() {
        $("#storeForm").submit(function () {
            //var mydata = $("#storeForm").serializeArray();

            // var mydata = JSON.stringify($("#storeForm").serializeArray());

            mydata: JSON.stringify({ Name: Name, StoreNumber: StoreNumber }),
            alert(mydata); // it's only for test
            debugger;
            $.ajax({
                
                url: '/Store/Create',
                contentType: 'application/json; charset=utf-8',
                type: 'POST',
                data: mydata,
                dataType: 'json',
                success: (function (res) {
                    alert("success");
                    $("#dvStoreList").html(res);
                }),
                error: function (res) {
                    alert("Error ");
                }
            });
        });
        alert("here is the jquery");
    }
