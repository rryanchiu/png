import {atom} from 'nanostores'
import {createStore} from 'idb-keyval'
import type {UseStore} from 'idb-keyval'

export const layerStore = atom<UseStore | null>(null)

export const initStores = async () => {
    layerStore.set(createStore('layers', 'keyval'))
}

export default async () => {
    await initStores()
    return null
}
