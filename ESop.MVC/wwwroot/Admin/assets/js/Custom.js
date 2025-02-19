
function confirmDelete(url) {
    // Use SweetAlert for confirmation  
    swal.fire({
        title: "آیا از حذف این محصول مطمئن هستید؟",
        text: "این عمل برگشت ناپذیر است.",
        icon: "warning",
        buttons: {
            cancel: "انصراف",
            confirm: {
                text: "حذف",
                value: true,
                visible: true,
                className: "bg-danger",
                closeModal: true
            }
        }
    }).then((willDelete) => {
        if (willDelete) {
            // If the user confirms, redirect to the delete action  
            window.location.href = url;
        }
    });
}  