﻿window.ShowToastr = (type, message) => {
    if (type === "success") {
        toastr.success(message, "Operation Completed", { timeOut: 10000})
    }
    if (type === "error") {
        toastr.error(message, "Operation Failed", { timeOut: 10000 })
    }
}