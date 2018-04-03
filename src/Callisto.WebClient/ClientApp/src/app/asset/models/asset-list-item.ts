export class AssetListItem {
    Id: number;
    Name: string;
    Description: string;
    Icon: string;
    ColorClass: string;
    Max: number;
    HasStatus: boolean;
    Current: number;
    Children: AssetListItem[];

    static NewInstance(id: number,
        name: string = 'Test1',
        desc: string = 'Test 2',
        icon: string = 'users',
        color: string = 'info'): AssetListItem {

        const num = Math.random();
        return {
            Id: id,
            ColorClass: color,
            Children: [],
            Current: Math.floor(Math.random() * 10),
            Max: 10,
            Icon: icon,
            HasStatus: true,
            Name: name,
            Description: desc
        };
    }
}
