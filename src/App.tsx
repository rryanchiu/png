import './App.css'
import Settings from "./pages/Settings.tsx";

const App = () => {
    return (
        <main class={'w-full h-full overflow-hidden  relative'} style="background-image: linear-gradient(90deg, #ffa9f930 0%, #ffa7ad40 0%, #ffa7ad20 50%, #ffa9f910 70%, #ffa9f900 100%);">
            <div id="canvasContainer" class={'w-full h-full'}></div>
            <Settings class={'absolute top-40 right-25'}/>
        </main>
    )
}

export default App
