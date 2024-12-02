function statusColor() {
    var statusElements = document.querySelectorAll('[id$="lblStatus"]'); // Select all elements that end with 'lblStatus' ID

    statusElements.forEach(function (statusElement) {
        var statusText = statusElement.innerText || statusElement.textContent;

        // Remove previous color classes
        statusElement.classList.remove('text-success', 'text-danger', 'text-warning', 'text-dark');

        switch (statusText.trim()) {
            case 'Confirmed':
                statusElement.classList.add('text-success!important');
                break;
            case 'Cancelled':
                statusElement.classList.add('text-danger!important');
                break;
            case 'Pending':
                statusElement.classList.add('text-warning!important');
                break;
            default:
                statusElement.classList.add('text-dark!important');
                break;
        }
    });
}

statusColor();
