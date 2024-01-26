export default class filter {
    execute() {
        console.log('filter executed.');

        $('#FilterModal input').each( function(i, el) {
            console.log(el.name);
            console.log($(el).val());
        });
    }
}