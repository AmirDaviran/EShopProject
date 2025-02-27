function fillPageId(pageNumber) {
    // گرفتن URL فعلی
    var currentUrl = window.location.href;

    // تجزیه URL برای دسترسی به Query String
    var url = new URL(currentUrl);
    var params = new URLSearchParams(url.search);

    // تنظیم یا به‌روزرسانی پارامتر Page
    params.set('Page', pageNumber);

    // ساخت URL جدید
    var newUrl = url.origin + url.pathname + '?' + params.toString();

    // بارگذاری صفحه جدید با URL به‌روزرسانی‌شده
    window.location.href = newUrl;
}