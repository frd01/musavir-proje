function layout_local_listeleri_yukle() {

    let local_listeler = window.localStorage.getItem("local_listeler")

    if (local_listeler != null) {

        local_listeler = JSON.parse(local_listeler)

        const hzl_gr_cm_cariler = $("#hzl_gr_cm_cariler").data("kendoComboBox")

        const layout_cm_firmalar = $("#layout_cm_firmalar").data("kendoComboBox")

        console.log("local_listeler : ", local_listeler)

        hzl_gr_cm_cariler.setDataSource(local_listeler.FirmaListesi)

        layout_cm_firmalar.setDataSource(local_listeler.FirmaListesi)
    }
}