
    $(function () {
        $('#ProductCategoryID').change(function () {
            $.getJSON('/Products/GetSubCategories/' + $('#ProductCategoryID').val(), function (data) {
                var items = '<option>Select</option>';
                $.each(data, function (i, subcategoryName) {
                    items += "<option value='" + subcategoryName.Value + "'>" + subcategoryName.Text + "</option>";
                });
                $('#ProductSubCategoryID').html(items);
            });
        });

        $('#UnitID').change(function () {
            $.getJSON('/Products/GetSize/' + $('#UnitID').val(), function (data) {
                var items = '<option>Select</option>';
                $.each(data, function (i, size) {
                    items += "<option value='" + size.Value + "'>" + size.Text + "</option>";
                });
                $('#SizeID').html(items);
            });
        });
    });


