export class Pagination<T> {
    items: T[];
    totalRecords: number;

    constructor(items: T[], totalRecords: number) {
        this.items = items;
        this.totalRecords = totalRecords;
    }
}