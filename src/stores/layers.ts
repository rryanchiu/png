import {get, set} from 'idb-keyval';
import {Layer} from "../types/Layer.ts";
import {createSignal} from "solid-js";

const createStore = () => {
    const initLayer = {
        id: 'id_0', index: 0, title: '图层0',
        type: 'color',
        value: 'linear-gradient(90deg, #ffa9f930 0%, #ffa7ad40 0%, #ffa7ad20 50%, #ffa9f910 70%, #ffa9f900 100%);'
    };
    const [layers, setLayers] = createSignal<Layer[]>([]);

    const save = async (items: Layer[]) => {
        console.log('save layers', items)
        await set('layers', items);
        getList()
    }

    const getList = async () => {
        const value = await get('layers');
        console.log('get layers', value)
        if (!value || value.length <= 0) {
            debugger
            value.push(initLayer)
            set('layers', value);
        }
        setLayers(value);
    }

    getList();

    return {layers, save, getList};
};

export default createStore;
