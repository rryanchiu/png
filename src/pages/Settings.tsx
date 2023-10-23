import {
    closestCenter,
    createSortable,
    DragDropProvider,
    DragDropSensors,
    DragOverlay,
    SortableProvider,
    useDragDropContext
} from "@thisbeyond/solid-dnd";
import {Layer} from "../types/Layer.ts";
import {createSignal, For} from "solid-js";
import createStore from "../stores/layers.ts";

interface SettingsProp {
    class?: string
}

interface SortItemProp {
    item: Layer,
    onDelete: () => void
    onChange: () => void
}

const SortItem = (props: SortItemProp) => {
    const sortable = createSortable(props.item.index);
    //@ts-ignore
    const [state] = useDragDropContext();

    const [item, setItem] = createSignal<Layer>(props.item);

    const data = item();

    const changeConfigType = (value: 'color' | 'text' | 'function') => {
        const currentData = item();
        currentData.type = value
        setItem(currentData)
        props.onChange()
    }

    return (
        <div
            //@ts-ignore
            use:sortable
            class={'sortable'}
            classList={{
                "opacity-25": sortable.isActiveDraggable,
                "transition-transform": !!state.active.draggable,
            }}
        >
            {/*<div class={'flex gap-2 hover:bg-gray-1 p2 b m2'}>*/}
            {/*    <i class={'ri-menu-line'}></i>*/}
            {/*    {props.item.title}*/}
            {/*</div>*/}
            <div class="relative collapse collapse-arrow hover:bg-gray-1  ">
                <input type="checkbox" class={"peer"}/>
                <div class="collapse-title text-xl font-medium peer-checked:bg-gray-1">
                    <div class={'flex gap-2'}>
                        <i class={'ri-menu-line'}></i>
                        {data.title}
                    </div>
                </div>
                <div class="collapse-content peer-checked:bg-gray-1">
                    <div class="tabs tabs-sm tabs-boxed">

                        <button class={data.type === 'color' ? 'tab tab-active' : 'tab'}
                                onClick={() => changeConfigType('color')}>颜色
                        </button>
                        <button class={data.type === 'text' ? 'tab tab-active' : 'tab'}
                                onClick={() => changeConfigType('text')}>文字
                        </button>
                        <button class={data.type === 'function' ? 'tab tab-active' : 'tab'}
                                onClick={() => changeConfigType('function')}>函数
                        </button>
                    </div>
                    <button class="btn btn-outline btn-error btn-sm w-full">
                        <i class="ri-delete-bin-5-line"></i>
                        <div onClick={props.onDelete}>删除图层
                        </div>
                    </button>
                </div>
            </div>
        </div>
    )
}

const Settings = (props: SettingsProp) => {
    const {layers, save} = createStore();

    const getClasses = () => {
        return props.class || ''
    }

    const [activeItem, setActiveItem] = createSignal<string | null>(null);

    const ids = () => layers().map((layer) => layer.index);

    const drawing = async () => {
        console.log('drawing')
    }

    const saveLayers = async (items: Layer[]) => {
        await save(items)
        await drawing();
    }

    const onDragStart = ({draggable}: any) => {
        setActiveItem(draggable.id);
    }

    const onDragEnd = ({draggable, droppable}: any) => {
        if (draggable && droppable) {
            const currentItems = ids();
            const fromIndex = currentItems.indexOf(draggable.id);
            const toIndex = currentItems.indexOf(droppable.id);
            if (fromIndex !== toIndex) {
                const updatedItems = currentItems.slice();
                updatedItems.splice(toIndex, 0, ...updatedItems.splice(fromIndex, 1));
                const oldItems = layers();
                const newItems = oldItems.sort((a, b) => {
                    const indexA = updatedItems.indexOf(a.index);
                    const indexB = updatedItems.indexOf(b.index);
                    return indexA - indexB;
                })

                saveLayers(newItems)
                console.log(JSON.stringify(ids()))
            }

        }
    };

    const addLayer = () => {
        const index = Math.max(...ids()) + 1;
        const newItem: Layer = {id: `id_${index}`, title: `图层${index}`, index: index};
        saveLayers([...layers(), newItem]);
    }

    const deleteLayer = async (index: number) => {
        const currentLayers = layers()
        if (currentLayers && currentLayers.length > 0) {
            currentLayers.splice(index, 1);
        }
        await saveLayers(currentLayers);
    }

    const changeLayer = async (index: number, item: Layer) => {
        const currentLayers = layers()
        currentLayers[index] = item;
        await saveLayers(currentLayers);
    }

    const changeSettingValue = (key: string, value: any) => {
        changeSettingValue
    }

    return (
        <div class={['min-w-xs h-auto min-h-sm   bg-white dark:bg-dark-1  shadow-2xl rd-5 p2  absolute bottom-40 right-25 flex flex-col ',
            getClasses()
        ].join(' ')}>

            <header class='justify-between flex items-center p2'>
                <div class="flex items-center gap-2">
                    <img src="../../src/assets/p.png" class="w-8"/>
                    <span class="font-bold text-lg">png</span>
                </div>
                <button class="btn">
                    <i class="ri-add-line"></i>
                    <div onClick={addLayer}>新建图层</div>
                </button>
            </header>
            <main class={'relative  bottom-0 pr-1 ml2 mr1  overflow-y-auto '}>
                <DragDropProvider
                    onDragStart={onDragStart}
                    onDragEnd={onDragEnd}
                    collisionDetector={closestCenter}
                >
                    <DragDropSensors/>
                    <div class="column self-stretch">
                        <SortableProvider ids={ids()}>
                            <For each={layers()}>{(item, index) => (
                                <SortItem item={item} onChange={() => {
                                    changeLayer(index(), item)
                                }} onDelete={() => {
                                    deleteLayer(index())
                                }}/>
                            )
                            }</For>
                        </SortableProvider>
                    </div>
                    <DragOverlay>
                        <div class="sortable">{activeItem()}</div>
                    </DragOverlay>
                </DragDropProvider>
            </main>
        </div>
    )
}

export default Settings
