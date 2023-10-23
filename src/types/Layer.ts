export interface Layer {
    id: string,
    index: number,
    title: string,
    type?: 'color' | 'text' | 'function' ,
    value?: string
}
