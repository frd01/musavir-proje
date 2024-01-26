import Person from './person'
import Filter from './filter'
import Utils from './utils'

class Init {
    constructor() {

    }

    start() {
        var url = window.location;
        $('ul.nav-sidebar a').filter(function() {
            return this.href == url;
        }).addClass('active');

        $('ul.nav-treeview a').filter(function() {
                return this.href == url;
            }).parentsUntil(".nav-sidebar > .nav-treeview")
            .css({ 'display': 'block' })
            .addClass('menu-open').prev('a')
            .addClass('active');

        $('[data-role="datepicker"]').inputmask({ alias: "datetime", inputFormat: "dd/mm/yyyy" });

        var contextmenu = `<ul id='grid-context-menu'>
                                <li id='excelExport'>Excel'e Aktar</li>
                            </ul>`;
        $('body').append(contextmenu);

        $("#grid-context-menu").kendoContextMenu({
            target: ".k-grid",
            filter: "td",
            select: function(e) {
                var row = $(e.target).parent()[0];
                var grid = $(row.closest('.k-grid')).data('kendoGrid');
                var item = e.item.id;

                switch (item) {
                    case "excelExport":
                        grid.saveAsExcel();
                        break;
                    default:
                        break;
                };
            }
        });
    }
}

const person = new Person("abc", "0");
const filter = new Filter();
const init = new Init();
const utils = new Utils();

export {
    person,
    filter,
    init,
    utils
}