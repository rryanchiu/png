import {createSignal, JSXElement} from 'solid-js';

interface CollapseProps {
    title: string;
    border?: boolean;
    children: JSXElement
}

const Collapse = (props: CollapseProps) => {
    const [isOpen, setIsOpen] = createSignal(false);

    const getBorderClass = () => {
        if (props.border === undefined || props.border === true) {
            return 'p5 b1'
        }
        return 'p5 b1'
    }

    const toggleCollapse = () => {
        setIsOpen(!isOpen());
    };

    return (
        <div class="collapse relative">
            <div class={['b border-gray hover:shadow-2xl border-amber ', getBorderClass()].join(' ')} onClick={toggleCollapse}>
                {props.title}
            </div>
            {isOpen() && (
                <div class={['a'].join(' ')}>
                    {props.children}
                </div>
            )}
        </div>
    );
};

export default Collapse;