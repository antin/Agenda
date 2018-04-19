export class Item2Agenda {
    /**
    Id: 2,
item: 28922,
type: 1,
agenda: 88,
description: "ככה בא לי",
lastUpdate: "2018-01-01T00:00:00",
opinion: 0.5,
importance: 1
     */
    constructor(
        public id: number,
        public item: number,
        public type: number,
        public agenda: number,
        public description: string,
        public lastUpdate: Date,
        public opinion: number,
        public importance: number
        
    ) {

    }

}