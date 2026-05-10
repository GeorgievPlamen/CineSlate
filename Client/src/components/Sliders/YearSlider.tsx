import { Slider } from '../ui/slider';

interface Props {
  years: number[];
  setYears: (value: number[]) => void;
  min?: number;
}

// TODO: add year ranges to filtering
function YearSlider({ years, setYears, min = 1950 }: Props) {
  return (
    <div className="mb-2 mt-1">
      <div className="flex justify-between mb-1.5">
        <span className="text-sm font-extralight">{years[0]}</span>
        <span className="text-sm font-extralight">{years[1]}</span>
      </div>
      <Slider
        value={years}
        onValueChange={setYears}
        min={min}
        max={new Date().getFullYear()}
        step={1}
        className="mx-auto w-full max-w-xs"
      />
    </div>
  );
}

export default YearSlider;
