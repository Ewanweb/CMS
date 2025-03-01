Dropzone.options.dropzoneUpload = { // dropzoneUpload نام id فرم شماست
    acceptedFiles: "image/*",
    maxFiles: 10,
    init: function () {
        this.on("success", function (file, response) {
            console.log("فایل با موفقیت آپلود شد:", response);
            // در اینجا می‌توانید عملیات‌های دیگر بعد از آپلود موفقیت‌آمیز را انجام دهید
        });

        this.on("error", function (file, errorMessage) {
            console.error("خطا در آپلود فایل:", errorMessage);
            // در اینجا می‌توانید پیغام خطا را به کاربر نمایش دهید
        });
    }
};