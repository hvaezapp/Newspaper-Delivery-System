﻿(function ($) {
    function Category() {
        var $this = this;

        function initilizeModel() {
            $("#modal").on('loaded.bs.modal', function (e) {

            }).on('hidden.bs.modal', function (e) {
                $(this).removeData('bs.modal');
            });
        }
        $this.init = function () {
            initilizeModel();
        }
    }
    $(function () {
        var self = new Category();
        self.init();
    })
}(jQuery))
