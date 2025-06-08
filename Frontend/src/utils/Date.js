export const today = () => new Date();
export const addDays = (date, days) => new Date(date.getTime() + days * 86_400_000);