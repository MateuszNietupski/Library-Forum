export const DateFormat = (dateString) => {
    const date = new Date(dateString)
    return date.toLocaleString('pl-PL',
        {
            hour: '2-digit',
            minute: '2-digit',
            day: '2-digit',
            month: '2-digit',
            year: 'numeric'
        });
}