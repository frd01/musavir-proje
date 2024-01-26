export default class utils {
    constructor() {

    }

    serializeObject(elem) {
        var o = {};
        var a = elem.serializeArray();
        $.each(a, function() {
            if (o[this.name]) {
                if (!o[this.name].push) {
                    o[this.name] = [o[this.name]];
                }
                o[this.name].push(this.value || '');
            } else {
                o[this.name] = this.value || '';
            }
        });
        return o;
    }

    check(val) {
        if (!val || val === '') {
            return false;
        } else {
            return true;
        }
    }
}