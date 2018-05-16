function DynamicProductPropertiesInit(url) {
    $('#tip-produs').on('change',
        function () {
            var selectedValue = this.value;
            $('#product-properties-container')
                .load(url + '?tipProdus=' + selectedValue,
                    function () {
                        $('select').formSelect();
                    });
        });
}


